using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlugNPlayBackend.Models;
using PlugNPlayBackend.Services;
using PlugNPlayBackend.Services.Interfaces;
using System.Diagnostics;
using Microsoft.Extensions.Configuration;

namespace PlugNPlayBackend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IConfiguration _config;

        public AuthController(IAuthService authService, IConfiguration configuration)
        {
            _authService = authService;
            _config = configuration;
        }

        [HttpPost("register")]
        public async Task<ActionResult> PostRegister(User userObj)
        {
            var response = await _authService.Register(userObj.Username, userObj.Password, userObj.Email);
            if (response)
                return Ok("User registered");

            return Conflict("Username taken");
        }

        [HttpPost("login")]
        public async Task<ActionResult> PostLogin(User userObj)
        {
            var response = await _authService.Login(userObj.Username, userObj.Password);
            Debug.WriteLine(response);

            if (response == null)
                return Conflict("Wrong credentials");

            return Ok(response);
        }

        [HttpPost("password")]
        public async Task<ActionResult> ChangePassword(User userObj)
        {
            var response = await _authService.PasswordUpdate(userObj.Username, userObj.Password);

            if (response == null)
                return Conflict("Something went wrong");

            return Ok("Password has been changed");
        }
    }
}
