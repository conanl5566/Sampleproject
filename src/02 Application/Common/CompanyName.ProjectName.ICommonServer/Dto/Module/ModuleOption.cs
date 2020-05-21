using CompanyName.ProjectName.Core;

namespace CompanyName.ProjectName.ICommonServer
{
    public class ModuleOption : Option
    {
        public long? ParentId { get; set; }

        public string FullName { get; set; }

        public bool? IsEnabled { get; set; }
    }
}