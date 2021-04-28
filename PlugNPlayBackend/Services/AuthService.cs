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
            var client = new MongoClient(config["PlugNPlayDatabaseSettings:PlugNPlayDBContext"]);
            var database = client.GetDatabase(settings.DatabaseName);

            //Establish link to database collection
                //_user = database.GetCollection<User>(settings.UsersCollectionName);

            //Injecct other class dependencies
            _friendlistService = friendlistService;
            _userService = userService;
            _hub = hub;
        }

        //Method to update password
        public User PasswordUpdate(string username, string password)
        {
            User updatedUserObj = _userService.Get(username);
            updatedUserObj.Password = password;
            _userService.Update(username, updatedUserObj);
            //_user.ReplaceOne(user => user.Username == username, updatedUserObj);
            return updatedUserObj;
        }

        //Method to log in
        public Token Login(string username, string password)
        {
            Token newToken = new Token();
            //Implement
            if(CheckUserExistance(username))
            {
                if (PasswordCheck(username,password))
                {
                    return newToken;
                }
            }
            return null;
        }

        //Method to register
        public bool Register (string username, string password, string email)
        {
            if(!CheckUserExistance(username))
            {
                User registerUserObj = new User()
                {
                    Username = username,
                    Email = email,
                    Password = password
                };
                registerUserObj.Password = _passwordHasher.HashPassword(registerUserObj, registerUserObj.Password);
                _userService.Create(registerUserObj);
                    //_users.InsertOne(registerUserObj);
                return true;
            }
            return false;
        }

        //Method to check if user is registered
        public bool CheckUserExistance(string username)
        {
            var user = _userService.Get(username);
                //var user = _user.Find<User>(user => user.Username == username).FirstOrDefault();
            Debug.WriteLine(username);
            if (user == null)
                return false;
            return true;
        }

        //Method to heck if user's password matches given password
        public bool PasswordCheck(string username, string password)
        {
            User checkUser = _userService.Get(username);
                // User checkUser = _userService.Find<User>(user => user.Username == username).FirstOrDefault();
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
