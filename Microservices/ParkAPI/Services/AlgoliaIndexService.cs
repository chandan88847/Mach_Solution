using Algolia.Search.Clients;
using ParkAPI.Model;

namespace ParkAPI.Services
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
        public async Task AddorUpdateIndexVehiclesAsync(Park park)
        {
            var records = new
            {
                ObjectID = park.VehicleNumber,
                park.ParkId,
                park.Location,
                park.VehicleDescription,
                park.AvailableHours,
                park.Address,
                park.ApplicationUserId,
                park.VehicleNumber,
                
                //park.VehicleImage,
                park.ExpectedReturnTime
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
