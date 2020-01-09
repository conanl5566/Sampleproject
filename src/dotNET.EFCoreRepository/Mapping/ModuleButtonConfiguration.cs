using dotNET.CommonServer;
using dotNET.CommonServer;
using dotNET.CommonServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace dotNET.CommonServer
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