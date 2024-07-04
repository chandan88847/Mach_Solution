using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ParkAPI.Model
{
    public class Park
    {
        [Key]
        public Guid ParkId { get; set; }
        
        [JsonProperty("objectID")]
        public string ApplicationUserId { get; set; } //foreignkey

        public string VehicleDescription {  get; set; }

        public string Location {  get; set; }

        public string VehicleNumber {  get; set; }

        public string Address {  get; set; }

        public double AvailableHours {  get; set; }

        public DateTime ExpectedReturnTime {  get; set; }

        public byte[] VehicleImage { get; set; }
    }
}
