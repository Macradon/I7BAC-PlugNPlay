using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlugNPlayBackend.Models.Interfaces
{
    public interface IGameQueue
    {
        public string Id { get; set; }
        public string QueueName { get; set; }
        public bool QueueFull { get; set; }
        public bool AddToQueue(string connectionId);
        public List<string> GetParticipants();
    }
}
