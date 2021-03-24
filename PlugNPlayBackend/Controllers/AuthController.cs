using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlugNPlayBackend.Models;
using PlugNPlayBackend.Services;
using System.Diagnostics;

namespace PlugNPlayBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserService _userService;

        public AuthController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public ActionResult<User> PostRegister(User user)
        {
            var newUser = new User();
            newUser.Username = user.Username;
            newUser.Password = user.Password;
            newUser.Email = user.Email;
            _userService.Create(newUser);

            return newUser;
        }

        [HttpPost("login")]
        public ActionResult<User> PostLogin(string username, string password)
        {
            User loginUser = new User();

            return Ok();
        }

        [HttpPost("password")]
        public ActionResult<User> ChangePassword(string username, string password)
        {

            return Ok();
        }
    }
}
