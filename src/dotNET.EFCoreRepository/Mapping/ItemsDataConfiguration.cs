using dotNET.CommonServer;
using dotNET.CommonServer;
using dotNET.CommonServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace dotNET.CommonServer
{
    public class ItemsDataConfiguration : EntityMappingConfiguration<ItemsData>
    {
        public override void Map(EntityTypeBuilder<ItemsData> b)
        {
            b.ToTable("ItemsData")
                .HasKey(p => p.Id);
        }
    }
}