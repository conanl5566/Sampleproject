using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration.Json;
using System;

namespace dotNET.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class Zconfig
    {
        #region 读取配置信息
        /// <summary>
        /// 读取配置信息
        /// </summary>
        /// <param name="name">key</param>
        /// <returns></returns>
        public static string Getconfig(string name)
        {
            IConfiguration config = new ConfigurationBuilder().Add(new JsonConfigurationSource { Path = "appsettings.json", ReloadOnChange = true }).Build();
            var appconfig = new ServiceCollection()
            .AddOptions()
            .Configure<SiteConfig>(config.GetSection("SiteConfig"))
            .BuildServiceProvider()
            .GetService<IOptions<SiteConfig>>()
            .Value;

            return appconfig.Configlist.FirstOrDefault(o => o.Key == name).Values;
        }
        #endregion
    }


    #region 读取配置相关类
    /// <summary>
    /// 
    /// </summary>
    public class SiteConfig
    {
        /// <summary>
        /// 
        /// </summary>
        public List<SiteConfiglist> Configlist { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class SiteConfiglist
    {
        /// <summary>
        /// 
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Values { get; set; }
    }


    #endregion
}
