using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PlugNPlayBackend.Services.Interfaces;

namespace PlugNPlayBackend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService _profileService;

        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        [HttpGet("profile")]
        public async Task<ActionResult> GetProfile(string username)
        {
            var userProfile = await _profileService.GetProfile(username);
            if (userProfile != null)
            {
                return Ok(userProfile);
            }
            else return Conflict("Could not fetch profile for user" + username);
        }
    }
}
