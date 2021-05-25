using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlugNPlayBackend.Models;
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
        public async Task<ActionResult> GetFriendlist(UserPair getPair)
        {
            var friendList = await _friendlistService.GetFriendlist(getPair.Username);
            if (friendList != null)
            {
                return Ok(friendList);
            }
            return Conflict("Could not fetch friendlist from " + getPair.Username);
        }

        [HttpPost("add")]
        public async Task<ActionResult> AddFriend(UserPair addPair)
        {
            var friendlist = await _friendlistService.AddFriend(addPair.Username, addPair.FriendUsername);
            if (friendlist != null)
            {
                return Ok(friendlist);
            }
            return Conflict("Could not add " + addPair.FriendUsername + "to friendlist");
        }

        [HttpPost("Remove")]
        public async Task<ActionResult> RemoveFriend(UserPair removePair)
        {
            var friendlist = await _friendlistService.RemoveFriend(removePair.Username, removePair.FriendUsername);
            if (friendlist != null)
            {
                return Ok(friendlist);
            }
            return null;
        }
    }
}
