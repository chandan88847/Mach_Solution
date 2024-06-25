using AuthenticationAPI.DTO;
using AuthenticationAPI.Models;
using Microsoft.EntityFrameworkCore;
using UserAPI.Data;
using UserAPI.Model;

namespace UserAPI.Services
{
    public class UserService
    {
        private readonly UserDbContext _context;

        public UserService(UserDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllUserProfilesAsync()
        {
            return await _context.UserProfiles.ToListAsync();
        }

        public async Task CreateUserProfileAsync(string applicationUserId)
        {
            User userFromDb = _context.UserProfiles.FirstOrDefault
               (u => u.ApplicationUserId.ToLower() == applicationUserId.ToLower());

            
            if (userFromDb == null)
            {
                User userProfile = new User
                {
                    ApplicationUserId = applicationUserId
                };
                await _context.UserProfiles.AddAsync(userProfile);
                await _context.SaveChangesAsync();                
            }
        }

        public async Task<User> GetUserProfileByApplicationIDAsync(string applicationUserId)
        {
            if (string.IsNullOrWhiteSpace(applicationUserId))
            {
                throw new ArgumentException("ApplicationUserId is required.", nameof(applicationUserId));
            }

            var user = await _context.UserProfiles.FirstOrDefaultAsync(u => u.ApplicationUserId == applicationUserId);

            if (user == null)
            {
                throw new KeyNotFoundException("User not found.");
            }
            return user;
        }

        public async Task<User> UpdateUserProfileAsync(User userProfile)
        {
            var existingProfile = await _context.UserProfiles.FindAsync(userProfile.UserProfileId);
            if (existingProfile == null)
            {
                return null; // or throw an exception
            }

            _context.Entry(existingProfile).CurrentValues.SetValues(userProfile);
            await _context.SaveChangesAsync();

            return existingProfile;
        }

        public async Task<bool> DeleteUserProfileAsync(int id)
        {
            var userprofile = await _context.UserProfiles.FindAsync(id);
            if (userprofile == null)
            {
                return false; // or throw an exception
            }

            _context.UserProfiles.Remove(userprofile);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
