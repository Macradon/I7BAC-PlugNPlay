using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlugNPlayBackend.Models;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using PlugNPlayBackend.Services.Interfaces;

namespace PlugNPlayBackend.Services
{
    public class UserService : IUserService
    {
        private readonly IMongoCollection<User> _users;

        //Contructor 
        public UserService(IPlugNPlayDatabaseSettings settings, IConfiguration config)
        {
            var client = new MongoClient(config["PlugNPlayDatabaseSettings:PlugNPlayDBContext"]);
            var database = client.GetDatabase(settings.DatabaseName);

            _users = database.GetCollection<User>(settings.UsersCollectionName);
        }

        //This sections implements CRUD operations for the service 
        public User Get(string username) =>
            _users.Find<User>(user => user.Username == username).FirstOrDefault();

        public User Create(User userObj)
        {
            userObj.Friendlist.Add("");
            userObj.GameStats.Add("");
            userObj.ConnectionID = null;
            _users.InsertOne(userObj);
            return userObj;
        }

        public void Update(string username, User userObj) =>
            _users.ReplaceOne(user => user.Username == username, userObj);

        public void Remove(string username) =>
            _users.DeleteOne(user => user.Username == username);
    }
}
