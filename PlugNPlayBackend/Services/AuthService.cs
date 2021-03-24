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

namespace PlugNPlayBackend.Services
{
    public class AuthService
    {
        private readonly IMongoCollection<User> _user;
        private readonly FriendlistService _friendlistService;

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
            return newToken;
        }

        public bool Register (string username, string password, string email)
        {
            //Implement check username
            if(!CheckUserExistance(username))
            {
                Debug.WriteLine("UserCheckPassed");
                User registerUser = new User();
                registerUser.Username = username;
                registerUser.Password = password;
                registerUser.Email = email;
                _user.InsertOne(registerUser);
                return true;
            }
            return false;
        }

        private bool CheckUserExistance(string username)
        {
            var user = _user.Find<User>(user => user.Username == username).FirstOrDefault();
            if (user == null)
                return false;
            return true;
        }

        private bool ExistanceCheck(string usernam)
        {
            //Same as CheckUserExistance -- need to refactor from ClassDiagram
            return true;
        }

        private bool PasswordCheck(string password)
        {
            //Implement check
            return false;
        }

        private Token GenerateToken(string username)
        {
            Token newToken = new Token();
            //Implement token generation
            return newToken;
        }
    }
}
