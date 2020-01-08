using dotNET.Dto;
using dotNET.Dto.Enum;
using System;
using System.ComponentModel.DataAnnotations;

namespace dotNET.Domain.Entities.Sys
{
    /// <summary>
    ///
    /// </summary>
    public class WebConfig : Entity, IEntity
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

        /// <summary>
        ///
        /// </summary>
        public DateTime CreatorTime { get; set; }

        /// <summary>
        ///
        /// </summary>
        public long? CreatorUserId { get; set; }

        /// <summary>
        /// 生成ID
        /// </summary>
        /// <returns></returns>
        public long CreateId()
        {
            return base.CreateId(EntityEnum.Department);
        }
    }
}