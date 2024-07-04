using Microsoft.EntityFrameworkCore;
using ParkAPI.Model;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace ParkAPI.Data
{
    public class ParkDbContext : DbContext
    {
        public ParkDbContext(DbContextOptions<ParkDbContext> options) : base(options)
        {
        }
        public DbSet<Park> ParkTable { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Park>()
                .HasKey(e => e.ParkId);

            modelBuilder.Entity<Park>()
                .Property(e => e.ApplicationUserId)
                .IsRequired();
        }
    }
}
