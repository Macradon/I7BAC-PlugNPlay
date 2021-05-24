using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using PlugNPlayBackend.Models;
using PlugNPlayBackend.Services;
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
            //private readonly IMongoCollection<User> _user;
        private readonly IFriendlistService _friendlistService;
        private readonly IUserService _userService;
        private readonly PasswordHasher<User> _passwordHasher = new PasswordHasher<User>();
        private IHubContext<GlobalHub> _hub;

        //Constructor
        public AuthService(IPlugNPlayDatabaseSettings settings, IConfiguration config, IFriendlistService friendlistService, IUserService userService, IHubContext<GlobalHub> hub)
        {
            //Use data from settings and configurations
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            //Establish link to database collection
                //_user = database.GetCollection<User>(settings.UsersCollectionName);

            //Injecct other class dependencies
            _friendlistService = friendlistService;
            _userService = userService;
            _hub = hub;
        }

        //Method to update password
        public async Task<User> PasswordUpdate(string username, string password)
        {
            User updatedUserObj = await _userService.Get(username);
            updatedUserObj.Password = password;
            _userService.Update(username, updatedUserObj);
            //_user.ReplaceOne(user => user.Username == username, updatedUserObj);
            return updatedUserObj;
        }

        //Method to log in
        public async Task<Token> Login(string username, string password)
        {
            Token newToken = new Token();
            //Implement
            var userExistance = await CheckUserExistance(username);
            if (!userExistance)
            {
                if (await PasswordCheck(username,password))
                {
                    return newToken;
                }
            }
            return null;
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
            Token newToken = new Token();
            //Implement token generation
            return newToken;
        }
    }
}
