using System;

namespace dotNET.Dto
{
    /// <summary>
    /// 登录日志
    /// </summary>
    public class LoginLogDto
    {
        public long Id { get; set; }

        /// <summary>
        /// 登录用户Id
        /// </summary>
        public long LoginId { get; set; }

        /// <summary>
        /// 登录类型 Saas / Agent /Member
        /// </summary>
        public string LoginType { get; set; }

        /// <summary>
        /// 登录IP
        /// </summary>
        public string IP { get; set; }

        /// <summary>
        /// 登录时间
        /// </summary>
        public DateTime CreatorTime { get; set; }
    }

    public class LoginLogDtoext : LoginLogDto
    {
        public string Loginname { get; set; }
    }
}