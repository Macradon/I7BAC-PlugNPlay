using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PlugNPlayBackend.Models;
using PlugNPlayBackend.Services.Interfaces;

namespace PlugNPlayBackend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GameStatController : ControllerBase
    {
        private readonly IUserService _userService;

        public GameStatController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("get")]
        public async Task<ActionResult> GetProfile(User userObj)
        {
            var user = await _userService.Get(userObj.Username);
            if (user != null)
            {
                return Ok(user.GameStats);
            }
            else return Conflict("Could not fetch profile for user" + userObj.Username);
        }
    }
}
