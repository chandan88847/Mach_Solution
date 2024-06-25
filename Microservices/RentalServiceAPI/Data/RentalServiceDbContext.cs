using Microsoft.EntityFrameworkCore;
using RentalServiceAPI.Models;

namespace RentalServiceAPI.Data
{
    public class RentalServiceDbContext : DbContext
    {
        public RentalServiceDbContext(DbContextOptions<RentalServiceDbContext> options) : base(options)
        {            
        }
        public DbSet<RentalDetails> RentalDetails { get; set; }
    }
}