using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using PlugNPlayBackend.Models;
using PlugNPlayBackend.Services;
using PlugNPlayBackend.Websockets;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using Microsoft.AspNetCore.Identity;

namespace PlugNPlayBackend.Services
{
    public class AuthService
    {
        private readonly IMongoCollection<User> _user;
        private readonly FriendlistService _friendlistService;
        private readonly PasswordHasher<User> _passwordHasher = new PasswordHasher<User>();

        public AuthService(IPlugNPlayDatabaseSettings settings, IConfiguration config, FriendlistService friendlistService)
        {
            _friendlistService = friendlistService;

            var client = new MongoClient(config["PlugNPlayDatabaseSettings:PlugNPlayDBContext"]);
            var database = client.GetDatabase(settings.DatabaseName);

            _user = database.GetCollection<User>(settings.UsersCollectionName);
        }

        public User PasswordUpdate(string username, string password)
        {
            User tempUser = _user.Find<User>(user => user.Username == username).FirstOrDefault();
            tempUser.Password = password;
            _user.ReplaceOne(user => user.Username == username, tempUser);
            return tempUser;
        }

        public Token Login(string username, string password)
        {
            Token newToken = new Token();
            //Implement
            Debug.WriteLine("Username check");
            if(CheckUserExistance(username))
            {
                Debug.WriteLine("Password check");
                if (PasswordCheck(username,password))
                {
                    Debug.WriteLine("Done check");
                    return newToken;
                }
            }
            Debug.WriteLine("Failed checks");
            return null;
        }

        public bool Register (string username, string password, string email)
        {
            if(!CheckUserExistance(username))
            {
                User registerUser = new User()
                {
                    Username = username,
                    Email = email,
                    Password = password
                };
                registerUser.Password = _passwordHasher.HashPassword(registerUser, registerUser.Password);
                _user.InsertOne(registerUser);
                return true;
            }
            return false;
        }

        private bool CheckUserExistance(string username)
        {
            var user = _user.Find<User>(user => user.Username == username).FirstOrDefault();
            Debug.WriteLine(username);
            if (user == null)
                return false;
            return true;
        }

        private bool ExistanceCheck(string usernam)
        {
            //Same as CheckUserExistance -- need to refactor from ClassDiagram
            return true;
        }

        private bool PasswordCheck(string username, string password)
        {
            User checkUser = _user.Find<User>(user => user.Username == username).FirstOrDefault();
            switch(_passwordHasher.VerifyHashedPassword(checkUser,checkUser.Password,password))
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

        private Token GenerateToken(string username)
        {
            Token newToken = new Token();
            //Implement token generation
            return newToken;
        }
    }
}
