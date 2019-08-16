using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace conan.Dto
{
    public class PaymentSettingModel
    {
        public long Id { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// 服务器异步通知页面路径 设置相对路径
        /// </summary>
        [Display(Name = "异步通知")]
        [StringLength(100)]
        public string NotifyUrl { get; set; }

        /// <summary>
        /// 页面跳转同步通知页面路径 设置相对路径
        /// </summary>
        [Display(Name = "同步通知")]
        [StringLength(100)]
        public string ReturnUrl { get; set; }
    }
}
