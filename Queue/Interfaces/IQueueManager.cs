using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlugNPlayBackend.Models.Interfaces;

namespace PlugNPlayBackend.Queue.Interfaces
{
    public interface IQueueManager
    {
        public Task<IGameQueue> AddToQueue(string gameID, string connectionID);
        public Task<IGameQueue> GetQueue(string roomName);
        public void RemoveQueue(string roomName);
        public string CreateQueueName(string gameID);
    }
}
