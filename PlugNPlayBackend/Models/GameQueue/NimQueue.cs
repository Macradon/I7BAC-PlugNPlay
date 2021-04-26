using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlugNPlayBackend.Models.Interfaces;

namespace PlugNPlayBackend.Models.GameQueue
{
    public class NimQueue : IGameQueue
    {
        public string GameID()
        {
            return "nim";
        }
    }
}
