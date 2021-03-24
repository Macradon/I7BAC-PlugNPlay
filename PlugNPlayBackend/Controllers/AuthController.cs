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
        public ActionResult PostRegister(User userObj)
        {
            var response = _authService.Register(userObj.Username, userObj.Password, userObj.Email);
            Debug.WriteLine("response is " + response);
            if (response)
                return Ok("User registered");

            return Conflict("Username taken");
        }

        [HttpPost("login")]
        public ActionResult PostLogin(User userObj)
        {
            var response = _authService.Login(userObj.Username, userObj.Password);

            if (response == null)
                return Conflict("Wrong credentials");

            return Ok("Logged In: " + response);
        }

        [HttpPost("password")]
        public ActionResult ChangePassword(User userObj)
        {
            var response = _authService.PasswordUpdate(userObj.Username, userObj.Password);

            if (response == null)
                return Conflict("Something went wrong");

            return Ok();
        }
    }
}
