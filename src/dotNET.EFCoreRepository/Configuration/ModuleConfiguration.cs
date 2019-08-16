using dotNET.Domain.Entities.Sys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace dotNET.EntityFrameworkCore
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
