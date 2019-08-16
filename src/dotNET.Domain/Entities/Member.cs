using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace conan.Domain.Entities
{
    public class Member : Entity, IEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string UserSecretkey { get; set; }

        /// <summary>
        /// 代理Id
        /// </summary>
        public long AgentId { get; set; } = 0;  //为空为saas后台管理员

        /// <summary>
        /// 余额
        /// </summary>
        public decimal Balance { get; set; }




        /// <summary>
        /// 生成ID
        /// </summary>
        /// <returns></returns>
        public long CreateId()
        {
            return base.CreateId(EntityEnum.Member);
        }
    }
}
