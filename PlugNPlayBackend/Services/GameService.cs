using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlugNPlayBackend.Models;
using PlugNPlayBackend.Services.Interfaces;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;

namespace PlugNPlayBackend.Services
{
    public class GameService : IGameService
    {
        private readonly IMongoCollection<Game> _games;

        //Contructor 
        public GameService(IPlugNPlayDatabaseSettings settings, IConfiguration config)
        {
            var client = new MongoClient(config["PlugNPlayDatabaseSettings:PlugNPlayDBContext"]);
            var database = client.GetDatabase(settings.DatabaseName);

            _games = database.GetCollection<Game>(settings.GamesCollectionName);
        }

        public async Task<List<Game>> GetAllGames()
        {
            var allGames = await _games.Find(_ => true).ToListAsync();
            if (allGames != null)
                return allGames;
            return null;
        }

        public Game GetGame(ObjectId Id) =>
            _games.Find(game => game._id == Id).FirstOrDefault();
    }
}
