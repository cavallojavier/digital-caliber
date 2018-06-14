using digital.caliber.model.Models;
using Microsoft.EntityFrameworkCore;

namespace digital.caliber.model.Data
{
    public class CaliberDbContext : DbContext
    {
        public CaliberDbContext(DbContextOptions<CaliberDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Log> Logs { get; set; }

        public DbSet<BoltonTotalRef> BoltonTotalRef { get; set; }

        public DbSet<BoltonPreviousRef> BoltonPreviousRef { get; set; }

        public DbSet<PontRef> PontRef { get; set; }

        public DbSet<MoyersRef> MoyersRef { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
