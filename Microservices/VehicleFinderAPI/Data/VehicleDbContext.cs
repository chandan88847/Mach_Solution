using Microsoft.EntityFrameworkCore;
using VehicleFinderAPI.Models;

namespace UserAPI.Data
{
    public class VehicleDbContext : DbContext
    {
        public VehicleDbContext(DbContextOptions<VehicleDbContext> options) : base(options)
        {
        }
        public DbSet<VehicleDetails> VehicleDetails { get; set; }
    }
}
