using System;
using System.Linq;
using Xunit;
using PlugNPlayBackend.Controllers;
using PlugNPlayBackend.Services.Interfaces;
using PlugNPlayBackend.Models;
using NSubstitute;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PlugNPlayBackend.UnitTest
{
    public class AuthControllerTests
    {
        [Fact]
        public async Task PostRegister_Username_Taken()
        {
            // Arrange
            var existingUser = new User()
            {
                Username = "IExist",
                Password = "arbitraryPassword",
                Email = "arbitraryEmail"
            };
            var authService = Substitute.For<IAuthService>();
            authService.Register("IExist", "arbitraryPassword", "arbitraryEmail").Returns(false);
            var configuration = Substitute.For<IConfiguration>();
            AuthController systemUnderTest = new AuthController(authService, configuration);

            // Act
            var actionResult = await systemUnderTest.PostRegister(existingUser);

            // Assert
            var result = Assert.IsType<ConflictObjectResult>(actionResult);
            Assert.Equal("Username taken", result.Value);
        }

        [Fact]
        public async Task PostRegister_Success()
        {
            var nonExistingUser = new User()
            {
                Username = "IDoNotExist",
                Password = "arbitraryPassword",
                Email = "arbitraryEmail"
            };
            var authService = Substitute.For<IAuthService>();
            authService.Register("IDoNotExist", "arbitraryPassword", "arbitraryEmail").Returns(true);
            var configuration = Substitute.For<IConfiguration>();
            AuthController systemUnderTest = new AuthController(authService, configuration);

            // Act
            var actionResult = await systemUnderTest.PostRegister(nonExistingUser);

            // Assert
            var result = Assert.IsType<OkObjectResult>(actionResult);
            Assert.Equal("User registered", result.Value);
        }
    }
}
