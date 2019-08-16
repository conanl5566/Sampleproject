/**************************************************************************
 * 作者：X   
 * 日期：2017.01.18   
 * 描述：
 * 修改记录：    
 * ***********************************************************************/
using Dapper;

namespace conan.Domain.Entities
{
    /// <summary>
    /// 代理商门户绑定域名
    /// </summary>
    public class VisitDomain : Entity, IEntity
    {
        [IgnoreUpdate]
        public long AgentId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Domain { get; set; }

        /// <summary>
        /// 默认域名为系统分别的三级域名
        /// </summary>
        public bool Default { get; set; }

        /// <summary>
        /// 状态  1=正常，2=未备案
        /// </summary>
        public int State { get; set; }

        /// <summary>
        /// 生成ID
        /// </summary>
        /// <returns></returns>
        public long CreateId()
        {
            return base.CreateId(EntityEnum.VisitDomain);
        }
    }
}
