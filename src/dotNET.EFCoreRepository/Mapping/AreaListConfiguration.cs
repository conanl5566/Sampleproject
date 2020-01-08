using dotNET.Domain.Entities.Sys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace dotNET.EntityFrameworkCore
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