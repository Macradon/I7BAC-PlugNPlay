using System.Threading.Tasks;
using MongoDB.Driver;
using PlugNPlayBackend.Models;
using PlugNPlayBackend.Hubs;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using PlugNPlayBackend.Services.Interfaces;

namespace PlugNPlayBackend.Services
{
    public class AuthService : IAuthService
    {
        //Variables
        private readonly IFriendlistService _friendlistService;
        private readonly IUserService _userService;
        private readonly PasswordHasher<User> _passwordHasher = new PasswordHasher<User>();

        //Constructor
        public AuthService(IPlugNPlayDatabaseSettings settings, IConfiguration config, IFriendlistService friendlistService, IUserService userService, IHubContext<GlobalHub> hub)
        {
            //Use data from settings and configurations
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            //Injecct other class dependencies
            _friendlistService = friendlistService;
            _userService = userService;
        }

        //Method to update password
        public async Task<User> PasswordUpdate(string username, string password)
        {
            User updatedUserObj = await _userService.Get(username);
            if (updatedUserObj != null)
            {
                updatedUserObj.Password = password;
                _userService.Update(username, updatedUserObj);
                return updatedUserObj;
            }
            return null;
        }

        //Method to log in
        public async Task<Token> Login(string username, string password)
        {
            var userExistance = CheckUserExistance(username).Result;
            if (userExistance)
            {
                if (await PasswordCheck(username, password))
                {
                    return GenerateToken(username);
                }
                var nullPassword = new Token("");
                nullPassword.JsonWebToken = "noPassword";
                return nullPassword;
            }
            var nullUser = new Token("");
            nullUser.JsonWebToken = "noUser";
            return nullUser;
        }

        //Method to register
        public async Task<bool> Register (string username, string password, string email)
        {
            var userExistance = await CheckUserExistance(username);
            if (!userExistance)
            {
                User registerUserObj = new User()
                {
                    Username = username,
                    Email = email,
                    Password = password
                };
                registerUserObj.Password = _passwordHasher.HashPassword(registerUserObj, registerUserObj.Password);
                await _userService.Create(registerUserObj);
                return true;
            }
            return false;
        }

        //Method to check if user is registered
        public async Task<bool> CheckUserExistance(string username)
        {
            var user = await _userService.Get(username);
            Debug.WriteLine(username);
            if (user != null)
                return true;
            return false;
        }

        //Method to check if user's password matches given password
        public async Task<bool> PasswordCheck(string username, string password)
        {
            User checkUser = await _userService.Get(username);
            switch (_passwordHasher.VerifyHashedPassword(checkUser,checkUser.Password,password))
            {
                case PasswordVerificationResult.Failed:
                    return false;
                case PasswordVerificationResult.Success:
                    return true;
                case PasswordVerificationResult.SuccessRehashNeeded:
                    return true;
                default:
                    return false;
            }
        }

        //Method to generate a token
        public Token GenerateToken(string username)
        {
            Token newToken = new Token(username);
            //Implement token generation
            return newToken;
        }
    }
}
