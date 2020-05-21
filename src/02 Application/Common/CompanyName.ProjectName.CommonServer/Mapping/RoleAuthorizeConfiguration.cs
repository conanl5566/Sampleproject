using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompanyName.ProjectName.CommonServer
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