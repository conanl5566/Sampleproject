using CompanyName.ProjectName.Core;
using CompanyName.ProjectName.ICommonServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompanyName.ProjectName.CommonServer
{
    public class DepartmentConfiguration : EntityMappingConfiguration<Department>
    {
        public override void Map(EntityTypeBuilder<Department> b)
        {
            b.ToTable("Department")
                .HasKey(p => p.Id);
        }
    }
}