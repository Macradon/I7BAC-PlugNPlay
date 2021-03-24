using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using PlugNPlayBackend.Models;
using PlugNPlayBackend.Services;
using PlugNPlayBackend.Websockets;
using Microsoft.Extensions.Configuration;

namespace PlugNPlayBackend.Services
{
    public class AuthService
    {
        private readonly IMongoCollection<User> _user;
        private readonly FriendlistService _friendlistService;
        private readonly GlobalHub _globalHub;

        public AuthService(IPlugNPlayDatabaseSettings settings, IConfiguration config, FriendlistService friendlistService, GlobalHub globalHub)
        {
            _friendlistService = friendlistService;
            _globalHub = globalHub;

            var client = new MongoClient(config["PlugNPlayDatabaseSettings:PlugNPlayDBContext"]);
            var database = client.GetDatabase(settings.DatabaseName);

            _user = database.GetCollection<User>(settings.UsersCollectionName);
        }

        public User PasswordUpdate(string username, string password)
        {

        }
    }
}
