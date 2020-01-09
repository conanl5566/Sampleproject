using Autofac;
using Autofac.Extensions.DependencyInjection;
using dotNET.Core;
using dotNET.Core.Cache;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;

namespace dotNET.CommonServer
{
    public static class AutofacExt
    {
        private static IContainer _container;

        public static IContainer InitAutofac(IServiceCollection services, Assembly executingAssembly)
        {
            var builder = new ContainerBuilder();
            builder.RegisterGeneric(typeof(BaseRepository<>)).As(typeof(IBaseRepository<>)).InstancePerDependency().PropertiesAutowired();
            builder.RegisterType<UnitWork>().As<IUnitWork>().InstancePerLifetimeScope().PropertiesAutowired();
            builder.RegisterType<MongoDBHelper>().As<IMongoDBHelper>().SingleInstance().PropertiesAutowired();
            var controllerType = typeof(ControllerBase);
            builder.RegisterAssemblyTypes(executingAssembly).Where(t =>
            controllerType.IsAssignableFrom(t) && t != controllerType).PropertiesAutowired();
            services.AddSingleton(typeof(ICacheService), new RedisCacheService());

            var assemblys = System.Reflection.Assembly.Load("dotNET.ICommonServer");
            var baseType = typeof(IDependency);
            builder.RegisterAssemblyTypes(assemblys)
             .Where(m => baseType.IsAssignableFrom(m) && m != baseType).PropertiesAutowired()
             .AsImplementedInterfaces().InstancePerDependency();

            if (services.All(u => u.ServiceType != typeof(IHttpContextAccessor)))
            {
                services.AddScoped(typeof(IHttpContextAccessor), typeof(HttpContextAccessor));
            }
            builder.Populate(services);
            _container = builder.Build();
            return _container;
        }

        /////// <summary>
        /////// 从容器中获取对象  AutofacExt.GetFromFac<IBaseRepository<AreaList>>()
        /////// hangfire   无法使用属性注入  只能使用构造函数 或者该方法
        /////// </summary>
        /////// <typeparam name="T"></typeparam>
        ////public static T GetFromFac<T>()
        ////{
        ////    return _container.Resolve<T>();
        ////}
    }
}