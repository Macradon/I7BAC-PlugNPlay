using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlugNPlayBackend.Services;
using PlugNPlayBackend.Models;

namespace PlugNPlayBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public ActionResult<User> Get(string username)
        {
            var user = _userService.Get(username);

            if ( user == null)
                return NotFound();

            return user;
        }

        [HttpPut]
        public IActionResult Update(User userObj)
        {
            var user = _userService.Get(userObj.Username);

            if (user == null)
                return NotFound();

            _userService.Update(userObj.Username, userObj);
            return NoContent();
        }

        [HttpDelete]
        public IActionResult Delete(string username)
        {
            var user = _userService.Get(username);
            if ( user == null )
                return NotFound();

            _userService.Remove(user.Username);
            return NoContent();
        }
    }
}
