using UserAPI.Data;
using UserAPI.Model;

namespace UserAPI.Services
{
    public class InterServiceCalls
    {
        private readonly HttpClient _httpClient;
        private readonly UserDbContext _context;

        public InterServiceCalls(HttpClient httpClient, UserDbContext context)
        {
            _httpClient = httpClient;
            _context = context;
        }

        public async Task<bool> UserEntityAsync(string ApplicationUserId)
        {
            // Validate IdentityUserId
            var response = await _httpClient.GetAsync($"http://identity-service/api/user/{ApplicationUserId}");
            if (!response.IsSuccessStatusCode)
            {
                return false;
            }

            var entity = new User
            {
                ApplicationUserId = ApplicationUserId,
                // Set other properties
            };

            _context.UserProfiles.Add(entity);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
