using Algolia.Search.Clients;
using Algolia.Search.Models.Search;
using Microsoft.EntityFrameworkCore;
using ParkAPI.Data;
using ParkAPI.Model;

namespace ParkAPI.Services
{
    public class ParkService
    {
        private readonly ParkDbContext _context;
        private readonly ISearchIndex _index;
        private readonly AlgoliaIndexService _algoliaIndexService;

        public ParkService(ParkDbContext context, ISearchClient searchClient, AlgoliaIndexService algoliaIndexService)
        {
            _context = context;
            _algoliaIndexService = algoliaIndexService;
            _index = searchClient.InitIndex("vehicles");
        }

        public async Task<IEnumerable<Park>> GetAllVehiclesAsync()
        {
            return await _context.ParkTable.ToListAsync();
        }

        //Not needed.
        public async Task<Park> GetVehicleByIdAsync(string itemId)
        {
            return await _context.ParkTable.FindAsync(itemId);
        }


        public async Task<Park> CreateVehicleAsync(Park park)
        {
            _context.ParkTable.Add(park);
            var response = await _context.SaveChangesAsync();
            if (response == null)
            {
                return park;
            }
            await _algoliaIndexService.AddorUpdateIndexVehiclesAsync(park);
            return park;
        }

        public async Task<Park> UpdateVehicleAsync(Park park)
        {
            _context.Entry(park).State = EntityState.Modified;
            var response = await _context.SaveChangesAsync();
            if (response == null)
            {
                return park;
            }
            await _algoliaIndexService.AddorUpdateIndexVehiclesAsync(park);
            return park;
        }

        public async Task<bool> DeleteVehicleAsync(string itemId)
        {
            var vehicle = await _context.ParkTable.FindAsync(itemId);
            if (vehicle == null)
            {
                return false;
            }

            _context.ParkTable.Remove(vehicle);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Park>> GetVehicleByLocationAsync(string location)
        {
            //return await _context.VehicleDetails.Where(v => v.Location == location).ToListAsync();

            var searchResponse = await _index.SearchAsync<Park>(new Query(location));

            return searchResponse.Hits;
        }
    }
}
