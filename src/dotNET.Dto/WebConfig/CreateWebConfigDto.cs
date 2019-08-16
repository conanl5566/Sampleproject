using dotNET.Dto.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace dotNET.Dto.WebConfig
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateWebConfigDto
    {
        /// <summary>
        /// 关键字
        /// </summary>
        [Required]
        public virtual string ConfigKey { get; set; }
        /// <summary>
        /// 缓存值
        /// </summary>
        [Required]
        public virtual string ConfigValue { get; set; }
        /// <summary>
        /// 环境配置
        /// </summary>
        [Required]
        public virtual ConfigEnvironmentEnum ConfigType { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public virtual string ConfigDetail { get; set; }

    }
}
