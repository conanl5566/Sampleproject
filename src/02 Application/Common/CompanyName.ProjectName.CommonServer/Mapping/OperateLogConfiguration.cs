using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompanyName.ProjectName.CommonServer
{
    public class OperateLogConfiguration : EntityMappingConfiguration<OperateLog>
    {
        public override void Map(EntityTypeBuilder<OperateLog> b)
        {
            b.ToTable("OperateLog")
                .HasKey(p => p.Id);
        }
    }
}