using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using UserAPI.Dto;
using UserAPI.Model;
using UserAPI.Services;

namespace UserAPI.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        private readonly InterServiceCalls _interServiceCalls;

        public UserController(UserService userService,InterServiceCalls interServiceCalls)
        {
            _userService = userService;
            _interServiceCalls = interServiceCalls;
        }

        [HttpPost("createuserprofileasync")]
        public async Task<ActionResult> CreateUserProfileasync([FromBody] ProfileDto applicationUserId)
        {
            // await _userService.CreateUserProfileAsync(applicationUserId);
            string Id= applicationUserId.UserId.ToString();
          
            await _userService.CreateUserProfileAsync(Id); ;
            return Ok();
        }

        // GET: api/UserProfile
        [HttpGet("getall")]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUserProfiles()
        {
            var profiles = await _userService.GetAllUserProfilesAsync();
            return Ok(profiles);
        }

        [HttpGet("GetUserProfileByApplicationID/{applicationUserId}")]
        public async Task<ActionResult<User>> GetUserProfileByApplicationID(string applicationUserId)
        {
            var profile = await _userService.GetUserProfileByApplicationIDAsync(applicationUserId);
            return Ok(profile);
        }

        // PUT: api/UserProfile
        [HttpPut("update")]
        public async Task<ActionResult<User>> UpdateUserProfile(UpdateDto updateDto)
        {
            User userProfile = new User { ApplicationUserId=updateDto.ApplicationUserId,
                                   DateOfBirth=updateDto.DateOfBirth,
                                   Gender=updateDto.Gender,
                                   Address=updateDto.Address,
                                   DrivingLicenseId=updateDto.DrivingLicenseId,
                                   City=updateDto.City,
            };


            var updatedProfile = await _userService.UpdateUserProfileAsync(userProfile);
            if (updatedProfile == null)
            {
                return NotFound();
            }
            return Ok(updatedProfile);
        }

        // DELETE: api/UserProfile/{id}
        [HttpDelete("deleteuser/{id}")]
        public async Task<ActionResult> DeleteUserProfile(int id)
        {
            var result = await _userService.DeleteUserProfileAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
