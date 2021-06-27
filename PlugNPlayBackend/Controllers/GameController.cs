using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PlugNPlayBackend.Services.Interfaces;
using MongoDB.Bson;

namespace PlugNPlayBackend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpGet("getAll")]
        public async Task<ActionResult> GetAllGames()
        {
            var games = await _gameService.GetAllGames();
            if (games != null)
                return Ok(games);
            return Conflict("Could not fetch games");
        }

        [HttpGet("get")]
        public async Task<ActionResult> Get(string Id)
        {
            var gameObjId = new ObjectId(Id);
            var game = _gameService.GetGame(gameObjId);
            if (game != null)
                return Ok(game);
            return Conflict("Could not fetch game");
        }
    }
}
