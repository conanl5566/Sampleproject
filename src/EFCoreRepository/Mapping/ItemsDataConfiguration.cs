using CompanyName.ProjectName.CommonServer;
using CompanyName.ProjectName.CommonServer;
using CompanyName.ProjectName.CommonServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompanyName.ProjectName.CommonServer
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