/**************************************************************************
 * 作者：X   
 * 日期：2017.01.18   
 * 描述：登录日志
 * 修改记录：    
 * ***********************************************************************/

using System;

namespace dotNET.Domain.Entities
{
    /// <summary>
    /// 登录日志
    /// </summary>
    public class LoginLog : Entity, IEntity
    {
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

        /// <summary>
        /// 生成ID
        /// </summary>
        /// <returns></returns>
        public long CreateId()
        {
            return base.CreateId(EntityEnum.LoginLog);
        }
    }
}
