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
