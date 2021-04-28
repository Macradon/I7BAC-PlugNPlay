using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlugNPlayBackend.Models.Interfaces;

namespace PlugNPlayBackend.Queue.Interfaces
{
    public interface IQueueManager
    {
        public IGameQueue AddToQueue(string gameID, string connectionID);
        public IGameQueue GetQueue(int index);
    }
}
