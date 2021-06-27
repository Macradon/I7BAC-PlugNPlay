using System.Collections.Generic;

namespace PlugNPlayBackend.Models
{
    public class Profile
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Picture { get; set; }
        public List<GameStat> GameStats { get; set; }
    }
}
