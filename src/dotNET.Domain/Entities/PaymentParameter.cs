/**************************************************************************
 * 作者：X   
 * 日期：2017.03.3  
 * 描述：支付相关参数设置
 * 修改记录：    
 * ***********************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace conan.Domain.Entities
{
    /// <summary>
    /// 支付相关参数设置
    /// </summary>
    public class PaymentParameter : Entity, IEntity
    {
        /// <summary>
        /// Saas提供支付方式ID
        /// </summary>
        public long PaymentSettingId { get; set; }

        /// <summary>
        /// 代理商Id
        /// </summary>
        public long AgentId { get; set; }

        /// <summary>
        /// 域名
        /// </summary>
        public string Domain { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public bool Enabled { get; set; }


        #region 支付宝
        /// <summary>
        /// 支付宝合作身份者ID
        /// </summary>
        public string Partner { get; set; }

        /// <summary>
        /// 收款支付宝账号
        /// </summary>
        public string SellerEmail { get; set; }
        #endregion

        #region 微信
        /// <summary>
        /// 绑定支付的APPID
        /// </summary>
        public string AppID { get; set; }
        /// <summary>
        /// 微信商户号
        /// </summary>
        public string MCHID { get; set; }
        /// <summary>
        /// 公众帐号secert（仅JSAPI支付的时候需要配置）
        /// </summary>
        public string AppSecret { get; set; }
        #endregion


        /// <summary>
        /// KEY
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreatorTime { get; set; }

        public long CreateId()
        {
            return base.CreateId(EntityEnum.PaySetting);
        }
    }
}
