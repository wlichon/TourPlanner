using Microsoft.EntityFrameworkCore;
using TourPlanner.Models;

namespace TourPlannerAPI.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Tour> Tours{ get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
    }
}
