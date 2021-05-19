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
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("get")]
        public async Task<ActionResult<User>> Get(string username)
        {
            var user = await _userService.Get(username);

            if ( user == null)
                return NotFound();

            return user;
        }

        [HttpPut]
        public async Task<ActionResult<User>> Update(User userObj)
        {
            var user = await _userService.Get(userObj.Username);

            if (user == null)
                return NotFound();

            _userService.Update(userObj.Username, userObj);
            return NoContent();
        }

        [HttpDelete]
        public async Task<ActionResult<User>> Delete(string username)
        {
            var user = await _userService.Get(username);
            if ( user == null )
                return NotFound();

            _userService.Remove(user.Username);
            return NoContent();
        }
    }
}
