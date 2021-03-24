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
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public ActionResult PostRegister(User user)
        {
            var newUser = new User();
            newUser.Username = user.Username;
            newUser.Email = user.Email;
            if(_authService.Register(user.Username,user.Password,user.Email))
            {
                return Ok();
            }

            return Conflict("Username taken");
        }

        [HttpPost("login")]
        public ActionResult PostLogin(string username, string password)
        {
            User loginUser = new User();
            _authService.Login(username, password);
            return Ok();
        }

        [HttpPost("password")]
        public ActionResult ChangePassword(string username, string password)
        {
            _authService.PasswordUpdate(username, password);
            return Ok();
        }
    }
}
