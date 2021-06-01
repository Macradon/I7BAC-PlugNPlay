using System.Collections.Generic;
using System.Threading.Tasks;
using PlugNPlayBackend.Models;
using MongoDB.Bson;

namespace PlugNPlayBackend.Services.Interfaces
{
    public interface IGameService
    {
        public Task<List<Game>> GetAllGames();
        public Task<Game> GetGame(ObjectId Id);
    }
}
