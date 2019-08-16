using dotNET.Domain.Entities;
using dotNET.Domain.Entities.Sys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace dotNET.Domain
{
 
    public class RoleAuthorizeConfiguration : EntityMappingConfiguration<RoleAuthorize>
    {
        public override void Map(EntityTypeBuilder<RoleAuthorize> b)
        {
            b.ToTable("RoleAuthorize")
                .HasKey(p => p.Id);
        }
    }

}
