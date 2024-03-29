﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PlugNPlayBackend.Models;
using PlugNPlayBackend.Services.Interfaces;
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

            switch (response.JsonWebToken)
            {
                case "noUser":
                    return NotFound("User not found");
                case "noPassword":
                    return Conflict("Wrong credentials");
                default:
                    return Ok(response);
            }
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
