using Autofac.Extensions.DependencyInjection;
using dotNET.Core;
using dotNET.CommonServer;
using dotNET.HttpApi.Host.Code;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using NLog.Extensions.Logging;
using NLog.Web;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace dotNET.HttpApi.Host
{
    /// <summary>
    ///
    /// </summary>
    public class Startup
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        ///
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="services"></param>
        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.Replace(ServiceDescriptor.Transient<IControllerActivator, ServiceBasedControllerActivator>());
            services.AddMvc().AddJsonOptions(options =>
              {
                  options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                  options.SerializerSettings.Converters.Add(
                        new Newtonsoft.Json.Converters.IsoDateTimeConverter()
                        {
                            DateTimeFormat = "yyyy-MM-dd HH:mm:ss"
                        }
                      );
                  //小写
                  options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                  options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                  //   //  options.SerializerSettings.DateFormatString = "yyyy-MM-dd";
              });
            //   services.AddMvc().AddXmlSerializerFormatters();
            //  services.AddMvc().AddXmlDataContractSerializerFormatters();
            services.AddLogging();
            services.AddCors(options =>
         options.AddPolicy("AllowSameDomain", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
     ));
            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new CorsAuthorizationFilterFactory("AllowSameDomain"));
            });

            #region Swagger

            services.AddSwaggerGen(c =>
          {
              c.SwaggerDoc("v1", new Info
              {
                  Version = "v1",
                  Title = "接口文档",
                  Description = "接口文档-基础",
                  TermsOfService = "https://example.com/terms",
                  Contact = new Contact
                  {
                      Name = "XXX1111",
                      Email = "XXX1111@qq.com",
                      Url = "https://example.com/terms"
                  }
                  ,
                  License = new License
                  {
                      Name = "Use under LICX",
                      Url = "https://example.com/license",
                  }
              });

              c.SwaggerDoc("v2", new Info
              {
                  Version = "v2",
                  Title = "接口文档",
                  Description = "接口文档-基础",
                  TermsOfService = "https://example.com/terms",
                  Contact = new Contact
                  {
                      Name = "XXX2222",
                      Email = "XXX2222@qq.com",
                      Url = "https://example.com/terms"
                  }
                   ,
                  License = new License
                  {
                      Name = "Use under LICX",
                      Url = "https://example.com/license",
                  }
              });
              c.OperationFilter<HttpHeaderOperationFilter>();
              c.DocumentFilter<HiddenApiFilter>();
              var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
              var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
              c.IncludeXmlComments(xmlPath);
              c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"dotNET.Dto.xml"));
          });

            #endregion Swagger

            #region MiniProfiler

            if (bool.Parse(Configuration["IsUseMiniProfiler"]))
            {
                //https://www.cnblogs.com/lwqlun/p/10222505.html
                services.AddMiniProfiler(options =>
    options.RouteBasePath = "/profiler"
 ).AddEntityFramework();
            }

            #endregion MiniProfiler

            services.AddDbContext<EFCoreDBContext>(options => options.UseMySql(Configuration["Data:MyCat:ConnectionString"]));
            var container = AutofacExt.InitAutofac(services, Assembly.GetExecutingAssembly());
            return new AutofacServiceProvider(container);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="loggerFactory"></param>
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);//这是为了防止中文乱码
            loggerFactory.AddNLog();//添加NLog
            env.ConfigureNLog("nlog.config");//读取Nlog配置文件
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            #region Swagger

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "接口文档-业务");//基础接口文档放后面后显示
                c.SwaggerEndpoint("/swagger/v2/swagger.json", "接口文档-基础");//业务接口文档首先显示
                c.RoutePrefix = string.Empty;//设置后直接输入IP就可以进入接口文档
                c.IndexStream = () => GetType().GetTypeInfo().Assembly.GetManifestResourceStream("dotNET.HttpApi.Host.index.html");
            });

            #endregion Swagger

            app.UseCors("AllowSameDomain");
            if (bool.Parse(Configuration["IsUseMiniProfiler"]))
            {
                app.UseMiniProfiler();
            }
            app.UseMvc();
        }
    }
}