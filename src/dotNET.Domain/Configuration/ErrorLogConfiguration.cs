using dotNET.Domain.Entities;
using dotNET.Domain.Entities.Sys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace dotNET.Domain
{
 
    public class ErrorLogConfiguration : EntityMappingConfiguration<ErrorLog>
    {
        public override void Map(EntityTypeBuilder<ErrorLog> b)
        {
            b.ToTable("ErrorLog")
                .HasKey(p => p.Id);
        }
    }

}
