using dotNET.CommonServer;
using dotNET.CommonServer;
using dotNET.CommonServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace dotNET.CommonServer
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