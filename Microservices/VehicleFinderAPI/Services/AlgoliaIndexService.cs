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

        public async Task IndexVehiclesAsync(IEnumerable<VehicleDetails> vehicles)
        {
            var records = vehicles.Select(v => new
            {
                ObjectID = v.ItemId,
                ApplicationId = v.ApplicationId,
                ItemName = v.ItemName,
                ItemDescription = v.ItemDescription,
                ItemType = v.ItemType,
                PurchaseDate = v.PurchaseDate,
                Condition = v.Condition,
                Status = v.Status,
                PricePerHour = v.PricePerHour,
                VehicleRNumber = v.VehicleRNumber,
                Insurance = v.Insurance,
                Location = v.Location
            });

            await _index.SaveObjectsAsync(records);
        }
    }
}
