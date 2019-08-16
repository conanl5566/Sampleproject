using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace conan.Dto
{
    /// <summary>
    /// 代理查询
    /// </summary>
    public class AgentOption : Option
    {
        public string Name { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string ContactNumber { get; set; }

        
        /// <summary>
        /// 状态 1正常，
        /// </summary>
        public bool? State { get; set; }
        


    }
}
