using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PlugNPlayBackend.Services.Interfaces;

namespace PlugNPlayBackend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FriendlistController : ControllerBase
    {
        private readonly IFriendlistService _friendlistService;

        public FriendlistController(IFriendlistService friendlistService)
        {
            _friendlistService = friendlistService;
        }

        [HttpGet("get")]
        public async Task<ActionResult> GetFriendlist(string username)
        {
            var friendList = await _friendlistService.GetFriendlist(username);
            if (friendList != null)
            {
                return Ok(friendList);
            }
            return NotFound("Could not fetch friendlist from " + username);
        }

        [HttpPost("Remove")]
        public async Task<ActionResult> RemoveFriend(string username, string friendUsername)
        {
            var friendlist = await _friendlistService.RemoveFriend(username, friendUsername);
            if (friendlist != null)
            {
                return Ok(friendlist);
            }
            return null;
        }
    }
}
