using CompanyName.ProjectName.Core;
using System;

namespace CompanyName.ProjectName.ICommonServer
{
    /// <summary>
    /// 登录日志
    /// </summary>
    public class LoginLogOption : Option
    {
        public string LoginId { get; set; }

        /// <summary>
        /// 登录类型 Saas / Agent /Member
        /// </summary>
        public string LoginType { get; set; }

        /// <summary>
        /// 所属代理
        /// </summary>
        public string AgentId { get; set; }

        /// <summary>
        /// 登录时间
        /// </summary>
        public DateTime? kCreatorTime { get; set; }

        /// <summary>
        /// 登录时间
        /// </summary>
        public DateTime? eCreatorTime { get; set; }
    }
}