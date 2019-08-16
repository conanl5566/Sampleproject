/**************************************************************************
 * 作者：X   
 * 日期：2017.01.18   
 * 描述：
 * 修改记录：    
 * ***********************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace conan.Domain.Entities
{
    /// <summary>
    /// 代理商
    /// </summary>
    public class Agent : Entity, IEntity
    {

        public string Name { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string ContactNumber { get; set; }


        public string Key { get; set; }

        public string Secret { get; set; }

        /// <summary>
        /// 状态 1正常，
        /// </summary>
        public bool State { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }


        /// <summary>
        /// 生成ID
        /// </summary>
        /// <returns></returns>
        public long CreateId()
        {
            return base.CreateId(EntityEnum.Agent);
        }
    }
}
