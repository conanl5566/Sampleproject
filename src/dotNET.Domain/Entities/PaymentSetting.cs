/**************************************************************************
 * 作者：X   
 * 日期：2017.03.3  
 * 描述：Saas提供支付方式设置
 * 修改记录：    
 * ***********************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace conan.Domain.Entities
{
    /// <summary>
    /// Saas提供支付方式设置
    /// </summary>
    public class PaymentSetting: Entity, IEntity
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// WxPay/Alipay
        /// </summary>
        public string PaymentType { get; set; }

        /// <summary>
        /// 服务器异步通知页面路径 设置相对路径
        /// </summary>
        public string NotifyUrl { get; set; }

        /// <summary>
        /// 页面跳转同步通知页面路径 设置相对路径
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreatorTime { get; set; }

        public long CreateId()
        {
            return base.CreateId(EntityEnum.PaymentSetting);
        }
    }
}
