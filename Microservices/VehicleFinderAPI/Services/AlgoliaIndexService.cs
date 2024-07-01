using Algolia.Search.Clients;
using VehicleFinderAPI.Models;

namespace VehicleFinderAPI.Services
{
    public class AlgoliaIndexService
    {
        private readonly ISearchClient _searchClient;
        private readonly ISearchIndex _index;

        public AlgoliaIndexService(ISearchClient searchClient)
        {
            _searchClient = searchClient;
            _index = _searchClient.InitIndex("vehicles");
        }       

        //When user create vehicla for rental.
        public async Task AddorUpdateIndexVehiclesAsync(VehicleDetails vehicles)
        {
            var records = new 
            {
                ObjectID = vehicles.ItemId,
                vehicles.ApplicationId,
                vehicles.ItemName,
                vehicles.ItemDescription,
                vehicles.ItemType,
                vehicles.PurchaseDate,
                vehicles.Condition,
                vehicles.Status,
                vehicles.PricePerHour,
                vehicles.VehicleRNumber,
                vehicles.Insurance,
                vehicles.Location
            };

            try
            {
                await _index.SaveObjectAsync(records);
                Console.WriteLine("Record updated successfully in Algolia.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating record: {ex.Message}");
            }
        

        }
    }
}
