using Microsoft.AspNetCore.Mvc;
using UserProfileAPI.Models;
using UserProfileAPI.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UserProfileAPI.Controllers
{
    [Route("api/userprofile")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly UserProfileService _userProfileService;

        public UserProfileController(UserProfileService userProfileService)
        {
            _userProfileService = userProfileService;
        }

        // GET: api/UserProfile
        [HttpGet("getall")]
        public async Task<ActionResult<IEnumerable<UserProfile>>> GetAllUserProfiles()
        {
            var profiles = await _userProfileService.GetAllUserProfilesAsync();
            return Ok(profiles);
        }

        // PUT: api/UserProfile
        [HttpPut("update")]
        public async Task<ActionResult<UserProfile>> UpdateUserProfile(UserProfile userProfile)
        {
            var updatedProfile = await _userProfileService.UpdateUserProfileAsync(userProfile);
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
            var result = await _userProfileService.DeleteUserProfileAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

    }
}
