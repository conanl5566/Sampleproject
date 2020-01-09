using dotNET.CommonServer;

using dotNET.CommonServer;
using dotNET.CommonServer;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace dotNET.CommonServer
{
    public class AreaListConfiguration : EntityMappingConfiguration<AreaList>
    {
        public override void Map(EntityTypeBuilder<AreaList> b)
        {
            b.ToTable("AreaList")
                .HasKey(p => p.Id);
        }
    }
}