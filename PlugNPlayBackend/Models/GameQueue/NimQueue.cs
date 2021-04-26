using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlugNPlayBackend.Models.Interfaces;

namespace PlugNPlayBackend.Models.GameQueue
{
    public class NimQueue : IGameQueue
    {
        public bool queueFull = false;

        public string GameID()
        {
            return "nim";
        }

        public string Player1 { get; set; }
        public string Player2 { get; set; }


    }
}
