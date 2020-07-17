using Microsoft.EntityFrameworkCore;

namespace CompanyName.ProjectName.Core
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
            var assemblys = System.Reflection.Assembly.Load("CompanyName.ProjectName.CommonServer");
            modelBuilder.AddEntityConfigurationsFromAssembly(assemblys);//GetType().Assembly
        }
    }
}