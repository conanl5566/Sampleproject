using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace conan.Saas.Framework
{
    public class Operator
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string DepartmentId { get; set; }
        public long RoleId { get; set; }
        public string LoginIPAddress { get; set; }
        public string LoginToken { get; set; }
        public DateTime LoginTime { get; set; }
        public bool IsSystem { get; set; }
    }
}
