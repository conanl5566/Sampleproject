using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotNET.Dto
{
    public class UserDto
    {
        public long Id { get; set; }
        /// <summary>
        /// 帐号
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 代理Id
        /// </summary>
        public long AgentId { get; set; } = 0;  //为空为saas后台管理员
        /// <summary>
        /// 部门ID
        /// </summary>
        public long? DepartmentId { get; set; }
        /// <summary>
        /// 用户角色
        /// </summary>
        public long RoleId { get; set; }
        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool IsSys { get; set; }
        /// <summary>
        /// 状态 1：正常，2：禁止登录
        /// </summary>
        public int State { get; set; }
        /// <summary>
        /// 最后一次登录时间
        /// </summary>
        public DateTime? LastLoginTime { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string Avatar { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool? DeleteMark { get; set; }
    }
}
