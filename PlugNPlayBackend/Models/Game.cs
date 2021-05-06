using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PlugNPlayBackend.Models
{
    public class Game
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId _id { get; set; }

        public string Name { get; set; }
        public string Link { get; set; }
        public string Picture { get; set; }
        public string Description { get; set; }
        public int Players { get; set; }
    }
}
