using dotNET.Domain.Entities;
using dotNET.Domain.Entities.Sys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace dotNET.Domain
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
