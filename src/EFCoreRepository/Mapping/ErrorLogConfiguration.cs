using CompanyName.ProjectName.CommonServer;
using CompanyName.ProjectName.CommonServer;
using CompanyName.ProjectName.CommonServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompanyName.ProjectName.CommonServer
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