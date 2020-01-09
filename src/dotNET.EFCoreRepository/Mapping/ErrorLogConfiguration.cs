using dotNET.CommonServer;
using dotNET.CommonServer;
using dotNET.CommonServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace dotNET.CommonServer
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