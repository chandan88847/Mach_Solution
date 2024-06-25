using Microsoft.EntityFrameworkCore;
using RentalAPI.Models;

namespace RentalAPI.Data
{
    public class RentalDbContext:DbContext
    {

        public RentalDbContext(DbContextOptions<RentalDbContext> options) : base(options)
        {
        }
        public DbSet<RentalDetails> rentaldetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<RentalDetails>()
                .HasKey(e => e.RentalId);

            modelBuilder.Entity<RentalDetails>()
                .Property(e => e.OwnerUserId)
                .IsRequired();

            modelBuilder.Entity<RentalDetails>()
              .Property(e => e.RenterUserId)
              .IsRequired();
        }
    }
}
