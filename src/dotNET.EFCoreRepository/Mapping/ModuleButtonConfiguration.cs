using dotNET.Domain.Entities.Sys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace dotNET.EntityFrameworkCore
{
    public class ModuleButtonConfiguration : EntityMappingConfiguration<ModuleButton>
    {
        public override void Map(EntityTypeBuilder<ModuleButton> b)
        {
            b.ToTable("ModuleButton")
                .HasKey(p => p.Id);
        }
    }
}