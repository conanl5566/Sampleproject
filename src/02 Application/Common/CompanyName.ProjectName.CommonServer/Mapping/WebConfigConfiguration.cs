using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompanyName.ProjectName.CommonServer
{
    public class WebConfigConfiguration : EntityMappingConfiguration<WebConfig>
    {
        public override void Map(EntityTypeBuilder<WebConfig> b)
        {
            b.ToTable("WebConfig")
                .HasKey(p => p.Id);
        }
    }
}