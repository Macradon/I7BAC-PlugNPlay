using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PlugNPlayBackend.Models
{
    public class GameStat
    {
        public string GameName { get; set; }
        public bool Won { get; set; }
    }
}
