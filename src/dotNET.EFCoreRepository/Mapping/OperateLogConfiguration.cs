using dotNET.CommonServer;
using dotNET.CommonServer;
using dotNET.CommonServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace dotNET.CommonServer
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