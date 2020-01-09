using Autofac.Extensions.DependencyInjection;
using dotNET.Core;
using dotNET.CommonServer;
using dotNET.Hangfire.Job.Code;
using Hangfire;
using Hangfire.Dashboard;
using Hangfire.Dashboard.BasicAuthorization;
using Hangfire.Redis;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using NLog.Web;
using System;
using System.Reflection;
using System.Text;

namespace dotNET.Hangfire.Job
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddOptions();
            services.Configure<SiteConfig>(Configuration.GetSection("SiteConfig"));
            services.AddLogging();
            services.AddResponseCompression();
            services.Replace(ServiceDescriptor.Transient<IControllerActivator, ServiceBasedControllerActivator>());
            services.AddMvc();
            GlobalStateHandlers.Handlers.Add(new SucceededStateExpireHandler(int.Parse(Configuration["Hangfire:JobExpirationTimeout"])));
            services.AddHangfire(x =>
            {
                var connectionString = Configuration["Data:Redis:ConnectionString"];
                x.UseRedisStorage(connectionString, new RedisStorageOptions()
                {
                    //活动服务器超时时间
                    InvisibilityTimeout = TimeSpan.FromMinutes(60),
                    Db = int.Parse(Configuration["Data:Redis:Db"])
                });
                x.UseDashboardMetric(DashboardMetrics.ServerCount)
                    .UseDashboardMetric(DashboardMetrics.RecurringJobCount)
                    .UseDashboardMetric(DashboardMetrics.RetriesCount)
                    .UseDashboardMetric(DashboardMetrics.AwaitingCount)
                    .UseDashboardMetric(DashboardMetrics.EnqueuedAndQueueCount)
                    .UseDashboardMetric(DashboardMetrics.ScheduledCount)
                    .UseDashboardMetric(DashboardMetrics.ProcessingCount)
                    .UseDashboardMetric(DashboardMetrics.SucceededCount)
                    .UseDashboardMetric(DashboardMetrics.FailedCount)
                    .UseDashboardMetric(DashboardMetrics.DeletedCount);
            });
            services.AddDbContext<EFCoreDBContext>(options => options.UseMySql(Configuration["Data:MyCat:ConnectionString"]));
            var container = AutofacExt.InitAutofac(services, Assembly.GetExecutingAssembly());
            return new AutofacServiceProvider(container);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            var filter = new BasicAuthAuthorizationFilter(
               new BasicAuthAuthorizationFilterOptions
               {
                   SslRedirect = false,          // 是否将所有非SSL请求重定向到SSL URL
                   RequireSsl = false,           // 需要SSL连接才能访问HangFire Dahsboard。强烈建议在使用基本身份验证时使用SSL
                   LoginCaseSensitive = false,   //登录检查是否区分大小写
                   Users = new[]
                   {
                        new BasicAuthAuthorizationUser
                        {
                            Login = Configuration["Hangfire:Login"] ,
                            PasswordClear= Configuration["Hangfire:PasswordClear"]
                        }
                   }
               });
            app.UseHangfireDashboard("", new DashboardOptions
            {
                Authorization = new[]
                {
                   filter
                },
            });
            //配置要处理的队列列表 ，如果多个服务器同时连接到数据库，会认为是分布式的一份子。可以通过这个配置根据服务器性能的按比例分配请求，不会导致服务器的压力。不配置则平分请求
            var jobOptions = new BackgroundJobServerOptions
            {
                Queues = new[] { "critical", "test", "default" },//队列名称，只能为小写  排在前面的优先执行
                WorkerCount = Environment.ProcessorCount * int.Parse(Configuration["Hangfire:ProcessorCount"]), //并发任务数  --超出并发数。将等待之前任务的完成  (推荐并发线程是cpu 的 5倍)
                ServerName = Configuration["Hangfire:ServerName"],//服务器名称
            };
            app.UseHangfireServer(jobOptions); //启动hangfire服务
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);//这是为了防止中文乱码
            loggerFactory.AddNLog();//添加NLog
            env.ConfigureNLog("nlog.config");//读取Nlog配置文件
            app.UseMvc();
        }
    }
}