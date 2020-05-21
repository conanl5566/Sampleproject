using CompanyName.ProjectName.Core;

namespace CompanyName.ProjectName.ICommonServer
{
    public class DepartmentOption : Option
    {
        public long? ParentId { get; set; } = 0;

        public string Name { get; set; }
    }

    public class ItemsDataOption : Option
    {
        public long? ParentId { get; set; } = 0;

        public string Name { get; set; }
    }

    public class areaOption : Option
    {
        public long? ParentId { get; set; } = 0;

        public string Name { get; set; }
    }
}