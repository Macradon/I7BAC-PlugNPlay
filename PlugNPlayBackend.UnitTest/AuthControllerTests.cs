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
using NSubstitute.ReturnsExtensions;

namespace PlugNPlayBackend.UnitTest
{
    public class AuthControllerTests
    {
        User existingUser;
        User nonExistingUser;
        User correctUser;
        IAuthService authService;
        IConfiguration configuration;

        public AuthControllerTests()
        {
            authService = Substitute.For<IAuthService>();
            configuration = Substitute.For<IConfiguration>();

            existingUser = new User()
            {
                Username = "IExist",
                Password = "ArbitraryPassword",
                Email = "ArbitraryEmail"
            };

            nonExistingUser = new User()
            {
                Username = "IDoNotExist",
                Password = "ArbitraryPassword",
                Email = "ArbitraryEmail"
            };

            correctUser = new User()
            {
                Username = "IExist",
                Password = "CorrectPassword",
                Email = "ArbitraryEmail"
            };
        }

        [Fact]
        public async Task PostRegister_UsernameTaken()
        {
            // Arrange
            authService.Register("IExist", "ArbitraryPassword", "ArbitraryEmail").Returns(false);
            AuthController systemUnderTest = new AuthController(authService, configuration);

            // Act
            var actionResult = await systemUnderTest.PostRegister(existingUser);

            // Assert
            var result = Assert.IsType<ConflictObjectResult>(actionResult);
            Assert.Equal("Username taken", result.Value);
        }

        [Fact]
        public async Task PostRegister_UserRegistered()
        {
            // Arrange
            authService.Register("IDoNotExist", "ArbitraryPassword", "ArbitraryEmail").Returns(true);
            AuthController systemUnderTest = new AuthController(authService, configuration);

            // Act
            var actionResult = await systemUnderTest.PostRegister(nonExistingUser);

            // Assert
            var result = Assert.IsType<OkObjectResult>(actionResult);
            Assert.Equal("User registered", result.Value);
        }

        [Fact]
        public async Task PostLogin_UsernameNotRegistered()
        {
            // Arrange
            authService.Login("IDoNotExist", "ArbitraryPassword").ReturnsNull();
            AuthController systemUnderTest = new AuthController(authService, configuration);

            // Act
            var actionResult = await systemUnderTest.PostLogin(nonExistingUser);

            // Assert
            var result = Assert.IsType<ConflictObjectResult>(actionResult);
            Assert.Equal("Wrong credentials", result.Value);
        }

        [Fact]
        public async Task PostLogin_WrongPassword()
        {
            // Arrange
            authService.Login("IExist", "ArbitraryPassword").ReturnsNull();
            AuthController systemUnderTest = new AuthController(authService, configuration);

            // Act
            var actionResult = await systemUnderTest.PostLogin(existingUser);

            // Assert
            var result = Assert.IsType<ConflictObjectResult>(actionResult);
            Assert.Equal("Wrong credentials", result.Value);
        }

        [Fact]
        public async Task PostLogin_LoggedIn()
        {
            // Arrange
            var token = new Token();
            authService.Login("IExist", "CorrectPassword").Returns(token);
            AuthController systemUnderTest = new AuthController(authService, configuration);

            // Act
            var actionResult = await systemUnderTest.PostLogin(correctUser);

            // Assert
            var result = Assert.IsType<OkObjectResult>(actionResult);
            // Token is not implemented yet, and therefore does not return an actual token,
            // but only the signature model name.
            Assert.Equal("Logged In: PlugNPlayBackend.Models.Token", result.Value);
        }

        [Fact]
        public async Task ChangePassword_Failed()
        {
            // Arrange
            authService.PasswordUpdate("IExist", "ArbitraryPassword").ReturnsNull();
            AuthController systemUnderTest = new AuthController(authService, configuration);

            // Act
            var actionResult = await systemUnderTest.ChangePassword(existingUser);

            // Assert
            var result = Assert.IsType<ConflictObjectResult>(actionResult);
            Assert.Equal("Something went wrong", result.Value);
        }

        [Fact]
        public async Task ChangePassword_Succeeded()
        {
            // Arrange
            User tempUser = new User();
            authService.PasswordUpdate("IExist", "CorrectPassword").Returns(tempUser);
            AuthController systemUnderTest = new AuthController(authService, configuration);

            // Act
            var actionResult = await systemUnderTest.ChangePassword(correctUser);

            // Assert
            var result = Assert.IsType<OkObjectResult>(actionResult);
            Assert.Equal("Password has been changed", result.Value);
        }
    }
}
