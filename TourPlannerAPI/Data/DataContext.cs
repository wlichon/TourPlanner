using Microsoft.EntityFrameworkCore;
using TourPlanner.Models;

namespace TourPlannerAPI.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Tour> Tours{ get; set; }
        public DbSet<TourLog> TourLog { get; set; }

        public DbSet<TourInfo> TourInfo { get; set; }

            /*
        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Create the shadow property
            var id = builder.Entity<TourLog>().Property<Guid>("TourId");

            // Create the relationship for cascade delete
            builder.Entity<Tour>()
                .HasMany(e => e.TourLogs)
                .WithOne()
                .HasForeignKey("TourId")
                .OnDelete(DeleteBehavior.Cascade);
        }
            */
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        /*
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tour>()
                .Has
        }
        */
    }
}
