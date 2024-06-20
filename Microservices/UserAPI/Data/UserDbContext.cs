using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using UserAPI.Model;

namespace UserAPI.Data
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {
        }
        public DbSet<User> UserProfiles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasKey(e => e.UserProfileId);

            modelBuilder.Entity<User>()
                .Property(e => e.ApplicationUserId)
                .IsRequired();
        }
    }
}
