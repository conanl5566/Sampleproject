using dotNET.Domain.Entities;
using dotNET.Domain.Entities.Sys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace dotNET.Domain
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
