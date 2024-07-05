using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentalServiceAPI.Dto;
using RentalServiceAPI.Models;
using RentalServiceAPI.Services;

namespace RentalServiceAPI.Controllers
{
    [Route("api/rental")]
    [ApiController]
    public class RentalController : ControllerBase
    {
        private readonly RentalService _rentalService;

        public RentalController(RentalService rentalService)
        {
            _rentalService = rentalService;
        }

        // GET: api/RentalDetails
        [HttpGet("getrentaldetails")]
        public async Task<ActionResult<IEnumerable<RentalDetails>>> GetAllRenatlDetails()
        {
            var rentalDetails = await _rentalService.GetAllRentalDetailsAsync();
            return Ok(rentalDetails);
        }

        // GET: api/RentalDetails/{id}
        [HttpGet("getrentaldetails/{id}")]
        public async Task<ActionResult<RentalDetails>> GetAllRenatlDetailsById(Guid id)
        {            
            var rentalDetails = await _rentalService.GetAllRentalDetailsByIdAsync(id);
            return Ok(rentalDetails);
        }

        [HttpGet("getrentaldetailsbyuserid/{id}")]
        public async Task<ActionResult<IEnumerable<RentalDetails>>> GetAllRenatlDetailsByUserId(string id)
        {
            var rentalDetails = await _rentalService.GetAllRenatlDetailsByUserId(id);
            return Ok(rentalDetails);
        }

        // POST: api/RentalDetails
        [HttpPost("createrentaldetails")]
        public async Task<ActionResult<RentalDetails>> CreateRentalDetails([FromBody] RentalDetailsDto rentalDetailsDto)
        {
            RentalDetails rentalDetails = new RentalDetails
            {
                OwnerUserId = rentalDetailsDto.OwnerUserId,
                RenterUserId = rentalDetailsDto.RenterUserId,
                VehicleRNumber = rentalDetailsDto.VehicleRNumber,
                RentedDate = rentalDetailsDto.RentedDate,
                Duration = rentalDetailsDto.Duration,
                TotalAmount = rentalDetailsDto.TotalAmount,
                paymentId = rentalDetailsDto.paymentId,
                PaymentStatus = rentalDetailsDto.paymentId != null ? true : false,
                RentingLocation=rentalDetailsDto.RentingLocation

            };
            var rentalDetail = await _rentalService.CreateRentalDetailsAsync(rentalDetails);

            return CreatedAtAction(nameof(GetAllRenatlDetailsById), new { id = rentalDetail.RentalId }, rentalDetail);
        }

        // PUT: api/RentalDetails/{id}
        [HttpPut("updaterentaldetails")]
        public async Task<IActionResult> UpdateRentalDetails([FromBody] RentalDetails rentalDetails)
        {
            var updatedRentalData = _rentalService.UpdateRentalServiceAsync(rentalDetails);
            if (updatedRentalData == null)
            {
                return NotFound();
            }
            return Ok(rentalDetails);
        }

        // DELETE: api/RentalDetails/{id}
        [HttpDelete("deleterentaldetails/{id}")]
        public async Task<IActionResult> DeleteRentalDetails(Guid id)
        {
            var result = await _rentalService.DeleteRentalDetailsAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
