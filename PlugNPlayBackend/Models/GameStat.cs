using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PlugNPlayBackend.Models
{
    public class GameStat
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string GameStatID { get; set; }

        public string GameName { get; set; }
        public bool Won { get; set; }
    }
}
