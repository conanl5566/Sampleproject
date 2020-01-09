using CompanyName.ProjectName.CommonServer;

using CompanyName.ProjectName.CommonServer;
using CompanyName.ProjectName.CommonServer;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompanyName.ProjectName.CommonServer
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