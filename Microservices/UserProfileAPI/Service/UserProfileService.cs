using Microsoft.EntityFrameworkCore;
using UserProfileAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UserProfileAPI.Services
{
    public class UserProfileService
    {
        private readonly UserProfileDbContext _context;

        public UserProfileService(UserProfileDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserProfile>> GetAllUserProfilesAsync()
        {
            return await _context.UserProfiles.ToListAsync();
        }

        public async Task<UserProfile> UpdateUserProfileAsync(UserProfile updatedProfile)
        {
            var existingProfile = await _context.UserProfiles.FindAsync(updatedProfile.Id);
            if (existingProfile == null)
            {
                return null; // or throw an exception
            }

            _context.Entry(existingProfile).CurrentValues.SetValues(updatedProfile);
            await _context.SaveChangesAsync();

            return existingProfile;
        }

        public async Task<bool> DeleteUserProfileAsync(int id)
        {
            var profile = await _context.UserProfiles.FindAsync(id);
            if (profile == null)
            {
                return false; // or throw an exception
            }

            _context.UserProfiles.Remove(profile);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
