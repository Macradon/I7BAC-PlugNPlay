using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PlugNPlayBackend.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Username { get; set; }
        public List<string> GameStats { get; set; }
        public List<string> Friendlist { get; set; }
        public string Password { get; set; }
        public string Email {get;set;}
        public string ConnectionID { get; set; }
    }
}
