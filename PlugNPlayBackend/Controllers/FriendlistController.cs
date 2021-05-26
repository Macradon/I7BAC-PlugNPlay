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
        public async Task<ActionResult> GetFriendlist(User userObj)
        {
            var friendList = await _friendlistService.GetFriendlist(userObj.Username);
            if (friendList != null)
            {
                return Ok(friendList);
            }
            return Conflict("Could not fetch friendlist from " + userObj.Username);
        }

        //[HttpPost("add")]
        //public async Task<ActionResult> AddFriend(string username, string friendUsername)
        //{
        //    var friendlist = await _friendlistService.AddFriend(username, friendUsername);
        //    if (friendlist != null)
        //    {
        //        return Ok(friendlist);
        //    }
        //    return Conflict("Could not add " + friendUsername + "to friendlist");
        //}

        [HttpPost("request")]
        public async Task<ActionResult> SendRequest(UserPair requestPair)
        {
            await _friendlistService.SendRequest(requestPair.Username, requestPair.FriendUsername);
            return Ok();
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
