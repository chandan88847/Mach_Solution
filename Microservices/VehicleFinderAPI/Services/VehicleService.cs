using Algolia.Search.Clients;
using Algolia.Search.Models.Search;
using Microsoft.EntityFrameworkCore;
using UserAPI.Data;
using VehicleFinderAPI.Models;

namespace VehicleFinderAPI.Services
{
    public class VehicleService
    {
        private readonly VehicleDbContext _context;
        private readonly ISearchIndex _index;

        public VehicleService(VehicleDbContext context, ISearchClient searchClient)
        {
            _context = context;
            _index = searchClient.InitIndex("vehicles");
        }

        public async Task<IEnumerable<VehicleDetails>> GetAllVehiclesAsync()
        {
            return await _context.VehicleDetails.ToListAsync();
        }

        public async Task<VehicleDetails> GetVehicleByIdAsync(string itemId)
        {
            return await _context.VehicleDetails.FindAsync(itemId);
        }

        public async Task<VehicleDetails> CreateVehicleAsync(VehicleDetails vehicleDetails)
        {
            _context.VehicleDetails.Add(vehicleDetails);
            await _context.SaveChangesAsync();
            return vehicleDetails;
        }

        public async Task<VehicleDetails> UpdateVehicleAsync(VehicleDetails vehicleDetails)
        {
            _context.Entry(vehicleDetails).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return vehicleDetails;
        }

        public async Task<bool> DeleteVehicleAsync(string itemId)
        {
            var vehicle = await _context.VehicleDetails.FindAsync(itemId);
            if (vehicle == null)
            {
                return false;
            }

            _context.VehicleDetails.Remove(vehicle);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<VehicleDetails>> GetVehicleByLocationAsync(string location)
        {
            //return await _context.VehicleDetails.Where(v => v.Location == location).ToListAsync();

            var searchResponse = await _index.SearchAsync<VehicleDetails>(new Query(location));

            return searchResponse.Hits;
        }
    }
}