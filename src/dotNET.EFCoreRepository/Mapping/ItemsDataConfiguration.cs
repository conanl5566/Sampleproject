using dotNET.Domain.Entities.Sys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace dotNET.EntityFrameworkCore
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