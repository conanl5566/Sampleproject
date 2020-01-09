using Microsoft.EntityFrameworkCore;

namespace CompanyName.ProjectName.CommonServer
{
    public partial class EFCoreDBContext : DbContext
    {
        public EFCoreDBContext(DbContextOptions<EFCoreDBContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.AddEntityConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}