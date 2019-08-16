using System;
using System.Collections.Generic;
using System.Text;

namespace dotNET.Dto
{
    public class ModuleOption : Option
    {
        public long? ParentId { get; set; }

        public string FullName { get; set; }

        public bool? IsEnabled { get; set; }
    }
}
