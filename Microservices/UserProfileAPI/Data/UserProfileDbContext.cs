using Microsoft.EntityFrameworkCore;
using UserProfileAPI.Models;

public class UserProfileDbContext : DbContext
{
    public UserProfileDbContext(DbContextOptions<UserProfileDbContext> options) : base(options)
    {
    }
    public DbSet<UserProfile> UserProfiles { get; set; }    
}
