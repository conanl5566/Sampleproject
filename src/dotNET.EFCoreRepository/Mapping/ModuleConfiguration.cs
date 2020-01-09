using dotNET.CommonServer;
using dotNET.CommonServer;
using dotNET.CommonServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace dotNET.CommonServer
{
    public class ModuleConfiguration : EntityMappingConfiguration<Module>
    {
        public override void Map(EntityTypeBuilder<Module> b)
        {
            b.ToTable("Module")
                .HasKey(p => p.Id);
        }
    }
}