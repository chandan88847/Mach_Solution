using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VehicleFinderAPI.Models;
using VehicleFinderAPI.Services;

namespace VehicleFinderAPI.Controllers
{
    [Route("api/vehicle")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly VehicleService _vehicleService;

        public VehicleController(VehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        // GET: api/VehicleDetails
        [HttpGet("getvehicles")]
        public async Task<ActionResult<IEnumerable<VehicleDetails>>> GetVehicles()
        {
            var vehicles = await _vehicleService.GetAllVehiclesAsync();
            return Ok(vehicles);
        }

        //To be deleted
        // GET: api/VehicleDetails/{id}
        [HttpGet("getvehicle/{id}")]
        public async Task<ActionResult<VehicleDetails>> GetVehicle(string id)
        {
            var vehicle = await _vehicleService.GetVehicleByIdAsync(id);

            if (vehicle == null)
            {
                return NotFound();
            }

            return Ok(vehicle);
        }

        // POST: api/VehicleDetails
        [HttpPost("createvehicle")]
        public async Task<ActionResult<VehicleDetails>> CreateVehicle(VehicleDetails vehicleDetails)
        {
            var createdVehicle = await _vehicleService.CreateVehicleAsync(vehicleDetails);
            return CreatedAtAction(nameof(GetVehicle), new { id = createdVehicle.ItemId }, createdVehicle);
        }

        // PUT: api/VehicleDetails/{id}
        [HttpPut("updatevehicle")]
        public async Task<IActionResult> UpdateVehicle([FromBody] VehicleDetails vehicleDetails)
        {
            var updatedVehicle = await _vehicleService.UpdateVehicleAsync(vehicleDetails);
            if (updatedVehicle == null)
            {
                return NotFound();
            }
            return Ok(updatedVehicle);
        }

        // DELETE: api/VehicleDetails/{id}
        [HttpDelete("deletevehice/{id}")]
        public async Task<IActionResult> DeleteVehicle(string id)
        {
            var result = await _vehicleService.DeleteVehicleAsync(id);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpGet("getvehiclebylocation/{location}")]
        public async Task<ActionResult<IEnumerable<VehicleDetails>>> GetVehicleByLocation(string location)
        {
            var vehicles = await _vehicleService.GetVehicleByLocationAsync(location);
            if (vehicles == null)
            {
                return NotFound();
            }
            return Ok(vehicles);
        }
    }
}