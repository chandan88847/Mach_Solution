using Microsoft.AspNetCore.Mvc;
using ParkAPI.Dto;
using ParkAPI.Model;
using ParkAPI.Services;

namespace ParkAPI.Controllers
{
    [Route("api/park")]
    [ApiController]
    public class ParkController : Controller
    {

            private readonly ParkService _parkService;
            private readonly IWebHostEnvironment _webHostEnvironment;

        public ParkController(ParkService parkService, IWebHostEnvironment webHostEnvironment)
            {
                    _parkService = parkService;
                    _webHostEnvironment = webHostEnvironment;
            }

            // GET: api/VehicleDetails
            [HttpGet("getpark")]
            public async Task<ActionResult<IEnumerable<Park>>> GetVehicles()
            {
                var vehicles = await _parkService.GetAllVehiclesAsync();
                return Ok(vehicles);
            }

            //To be deleted
            // GET: api/VehicleDetails/{id}
            [HttpGet("getvehicle/{id}")]
            public async Task<ActionResult<Park>> GetVehicle(string id)
            {
                var vehicle = await _parkService.GetVehicleByIdAsync(id);

                if (vehicle == null)
                {
                    return NotFound();
                }

                return Ok(vehicle);
            }

            // POST: api/VehicleDetails
            [HttpPost("createpark")]
            public async Task<ActionResult<Park>> CreateVehicle([FromForm] ParkDto parkDto)
            {
                try
                {
                    Park park = new Park
                    {
                        ApplicationUserId = parkDto.ApplicationUserId,
                        VehicleDescription = parkDto.VehicleDescription,
                        Location = parkDto.Location,
                        VehicleNumber = parkDto.VehicleNumber,
                        Address = parkDto.Address,
                        AvailableHours = parkDto.AvailableHours,
                        ExpectedReturnTime = parkDto.ExpectedReturnTime,

                    };

                    using (var memoryStream = new MemoryStream())
                    {
                         await parkDto.VehicleImage.CopyToAsync(memoryStream);
                         park.VehicleImage = memoryStream.ToArray();
                     }

                     var createdVehicle = await _parkService.CreateVehicleAsync(park);
                     return Ok(CreatedAtAction(nameof(GetVehicle), new { id = createdVehicle.ParkId }, createdVehicle));
                 }
                catch (Exception ex)
                {
                return BadRequest(ex.Message);
                }

            }

            // PUT: api/VehicleDetails/{id}
            [HttpPut("updatepark")]
            public async Task<IActionResult> UpdateVehicle([FromBody] Park park)
            {
                var updatedVehicle = await _parkService.UpdateVehicleAsync(park);
                if (updatedVehicle == null)
                {
                    return NotFound();
                }
                return Ok(updatedVehicle);
            }

            // DELETE: api/VehicleDetails/{id}
            [HttpDelete("deletepark/{id}")]
            public async Task<IActionResult> DeleteVehicle(string id)
            {
                var result = await _parkService.DeleteVehicleAsync(id);

                if (!result)
                {
                    return NotFound();
                }

                return NoContent();
            }

            [HttpGet("getvehiclebylocation/{location}")]
            public async Task<ActionResult<IEnumerable<Park>>> GetVehicleByLocation(string location)
            {
                var vehicles = await _parkService.GetVehicleByLocationAsync(location);
                if (vehicles == null)
                {
                    return NotFound();
                }
                return Ok(vehicles);
            }
        
    }
}
