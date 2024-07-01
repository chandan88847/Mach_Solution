using Algolia.Search.Clients;
using Algolia.Search.Models.Search;
using Microsoft.EntityFrameworkCore;
using UserAPI.Data;
//using VehicleFinderAPI.Migrations;
using VehicleFinderAPI.Models;

namespace VehicleFinderAPI.Services
{
    public class VehicleService
    {
        private readonly VehicleDbContext _context;
        private readonly ISearchIndex _index;
        private readonly AlgoliaIndexService _algoliaIndexService;

        public VehicleService(VehicleDbContext context, ISearchClient searchClient, AlgoliaIndexService algoliaIndexService)
        {
            _context = context; 
            _algoliaIndexService = algoliaIndexService;
            _index = searchClient.InitIndex("vehicles");           
        }

        public async Task<IEnumerable<VehicleDetails>> GetAllVehiclesAsync()
        {
            return await _context.VehicleDetails.ToListAsync();
        }

        //Not needed.
        public async Task<VehicleDetails> GetVehicleByIdAsync(string itemId)
        {
            return await _context.VehicleDetails.FindAsync(itemId);
        }


        public async Task<VehicleDetails> CreateVehicleAsync(VehicleDetails vehicleDetails)
        {
            _context.VehicleDetails.Add(vehicleDetails);
            var response = await _context.SaveChangesAsync();
            if (response == null)
            {
                return vehicleDetails;
            }
            await _algoliaIndexService.AddorUpdateIndexVehiclesAsync(vehicleDetails);
            return vehicleDetails;
        }

        public async Task<VehicleDetails> UpdateVehicleAsync(VehicleDetails vehicleDetails)
        {
            _context.Entry(vehicleDetails).State = EntityState.Modified;
            var response = await _context.SaveChangesAsync();
            if (response == null)
            {
                return vehicleDetails;
            }
            await _algoliaIndexService.AddorUpdateIndexVehiclesAsync(vehicleDetails);
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