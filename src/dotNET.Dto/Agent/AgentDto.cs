using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace conan.Dto
{
    /// <summary>
    /// 代理列表
    /// </summary>
    public class AgentDto
    {
        public long Id { get; set; }
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
    }
}
