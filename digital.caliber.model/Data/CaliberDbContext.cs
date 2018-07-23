using System.Linq;
using digital.caliber.model.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace digital.caliber.model.Data
{
    public class CaliberDbContext : IdentityDbContext<ApplicationUser>
    {
        public CaliberDbContext(DbContextOptions<CaliberDbContext> options) : base(options)
        {
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public DbSet<Log> Logs { get; set; }

        //public DbSet<BoltonTotalRef> BoltonTotalRef { get; set; }

        //public DbSet<BoltonPreviousRef> BoltonPreviousRef { get; set; }

        //public DbSet<PontRef> PontRef { get; set; }

        //public DbSet<MoyersRef> MoyersRef { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //foreach (var property in modelBuilder.Model.GetEntityTypes()
            //                            .SelectMany(t => t.GetProperties())
            //                            .Where(p => p.ClrType == typeof(decimal)))
            //{
            //    property.Relational().ColumnType = "decimal(5, 3)";
            //}
        }
    }
}
