using dotNET.Domain.Entities.Sys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace dotNET.EntityFrameworkCore
{
 
    public class RoleConfiguration : EntityMappingConfiguration<Role>
    {
        public override void Map(EntityTypeBuilder<Role> b)
        {
            b.ToTable("Role")
                .HasKey(p => p.Id);
        }
    }

}
