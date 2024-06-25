using Microsoft.AspNetCore.Mvc;
using RentalAPI.Dto;
using RentalAPI.Models;
using RentalAPI.Services;

namespace RentalAPI.Controllers
{
    [Route("api/rental")]
    [ApiController]
    public class RentalController : ControllerBase
    {
        private readonly RentalService _RentalService;


        public RentalController(RentalService rentalService)
        {
            _RentalService=rentalService;
        }


        [HttpPost("rentalservice")]
        public async Task<ActionResult> CreateUserProfileasync([FromBody] RentalDetails rentalDetails)
        {
           
            await _RentalService.CreateRentalAsync(rentalDetails);
            
            return Ok();
        }



    }
}
