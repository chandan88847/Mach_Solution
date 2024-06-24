using AuthenticationAPI.Data;
using Microsoft.EntityFrameworkCore;
using UserAPI.Data;
using VehicleFinderAPI.Services;

namespace VehicleFinderAPI.DataSeederScript
{
    public class DataSeeder
    {
        private readonly VehicleDbContext _context;
        private readonly AlgoliaIndexService _algoliaIndexService;

        public DataSeeder(VehicleDbContext context, AlgoliaIndexService algoliaIndexService)
        {
            _context = context;
            _algoliaIndexService = algoliaIndexService;
        }

        public async Task SeedAsync()
        {
            var vehicles = await _context.VehicleDetails.ToListAsync();
            await _algoliaIndexService.IndexVehiclesAsync(vehicles);
        }
    }
}
