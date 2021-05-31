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
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _users = database.GetCollection<User>(settings.UsersCollectionName);
        }

        //This sections implements CRUD operations for the service 
        public async Task<User> Get(string username) =>
            _users.Find<User>(user => user.Username == username).FirstOrDefault();

        public async Task<User> GetByConnection(string connectionID) =>
            _users.Find<User>(user => user.ConnectionID == connectionID).FirstOrDefault();

        public async Task<User> Create(User userObj)
        {
            List<string> emptyList = new List<string>();
            userObj.FriendRequests = emptyList;
            userObj.Friendlist = emptyList;
            userObj.GameStats = new List<GameStat>();
            _users.InsertOne(userObj);
            return userObj;
        }

        public void Update(string username, User userObj) =>
            _users.ReplaceOne(user => user.Username == username, userObj);

        public void Remove(string username) =>
            _users.DeleteOne(user => user.Username == username);
    }
}
