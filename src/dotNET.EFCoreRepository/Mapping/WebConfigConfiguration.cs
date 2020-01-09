using dotNET.CommonServer;
using dotNET.CommonServer;
using dotNET.CommonServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace dotNET.CommonServer
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