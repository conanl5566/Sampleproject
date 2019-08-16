using System;
using System.Collections.Generic;
using System.Text;

namespace dotNET.Dto
{
    public partial class RoleEdit
    {

        public long Id { get; set; }

        public string Name { get; set; }

        public bool? AllowEdit { get; set; } = false;

        public bool? AllowDelete { get; set; } = false;

        public int? SortCode { get; set; } = 0;

        public string Description { get; set; }

        public List<long> PermissionIds { get; set; }
    }
}
