using System;
using System.ComponentModel.DataAnnotations;

namespace dotNET.Dto
{
    /// <summary>
    /// 注册模型
    /// </summary>
    public class Register
    {
        /// <summary>
        /// (必填)帐号
        /// </summary>
        [Display(Name = "帐号")]
        [Required]
        public string Account { get; set; }

        /// <summary>
        /// (必填)密码
        /// </summary>
        [Display(Name = "密码")]
        [Required(ErrorMessage = "{0}是必填项")]
        public string Password { get; set; }

        /// <summary>
        /// (必填)验证码
        /// </summary>
        [Required]
        public string Code { get; set; }
    }

    /// <summary>
    /// 登录返回结果
    /// </summary>
    public class Login
    {
        /// <summary>
        /// 授权标志
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 帐号
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string HeadImg { get; set; }

        /// <summary>
        /// 会员等级
        /// </summary>
        public string Grade { get; set; }

        /// <summary>
        /// 是否有设置密码
        /// </summary>
        public bool NotPassword { get; set; }

        /// <summary>
        /// 失效时间
        /// </summary>
        public DateTime? FailureTime { get; set; }
    }
}