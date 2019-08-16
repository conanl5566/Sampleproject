using dotNET.Dto.Enum;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotNET.Domain.Entities.Sys
{
    /// <summary>
    /// 管理员
    /// </summary>
    public class User : Entity, IEntity
    {
        /// <summary>
        /// 帐号
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string UserSecretkey { get; set; }
        /// <summary>
        /// 名字
        /// </summary>
        public string RealName { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public string Tel { get; set; }

     
        /// <summary>
        /// 部门ID
        /// </summary>
        public long? DepartmentId { get; set; }
        /// <summary>
        /// 用户角色
        /// </summary>
        public long RoleId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool IsSys { get; set; }
        /// <summary>
        /// 状态 1：正常，0：禁止登录
        /// </summary>
        public int State { get; set; }
        /// <summary>
        /// 最后一次登录时间
        /// </summary>
        public DateTime? LastLoginTime { get; set; }
        /// <summary>
        /// 密码错误次数（正确一次后清0）
        /// </summary>
        public int PasswordErrorCount { get; set; } = 0;
        /// <summary>
        /// 头像
        /// </summary>
        public string Avatar { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool? DeleteMark { get; set; }

        public DateTime CreatorTime { get; set; }

        public long? CreatorUserId { get; set; }

        public DateTime? DeleteTime { get; set; }

        public long DeleteUserId { get; set; }

        /// <summary>
        /// 生成ID
        /// </summary>
        /// <returns></returns>
        public long CreateId()
        {
            return base.CreateId(EntityEnum.User);
        }
    }
}
