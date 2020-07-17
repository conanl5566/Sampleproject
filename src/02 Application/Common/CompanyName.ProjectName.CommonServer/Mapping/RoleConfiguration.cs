using CompanyName.ProjectName.Core;
using CompanyName.ProjectName.ICommonServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompanyName.ProjectName.CommonServer
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