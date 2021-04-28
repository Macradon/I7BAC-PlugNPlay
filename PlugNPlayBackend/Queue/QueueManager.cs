using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlugNPlayBackend.Queue.Interfaces;
using PlugNPlayBackend.Models.Interfaces;
using PlugNPlayBackend.Models.GameQueue;

namespace PlugNPlayBackend.Queue
{
    public class QueueManager : IQueueManager
    {
        private static List<IGameQueue> _gameQueues;

        public QueueManager()
        {
            _gameQueues = new List<IGameQueue>();
        }

        public IGameQueue AddToQueue(string gameID, string connectionID)
        {
            var gameQueue = _gameQueues.Find(queue => queue.Id == gameID);
            gameQueue.AddToQueue(connectionID);
            return gameQueue;
        }

        public IGameQueue GetQueue(int index)
        {
            if (index > _gameQueues.Count - 1)
                return null;
            return _gameQueues.ElementAt(index);
        }

        private IGameQueue CreateNewQueue(string gameID, string connectionId)
        {
            switch(gameID)
            {
                case "60893b5f3665f82c430c5d35":
                    var newQueue = new NimQueue();
                    newQueue.AddToQueue(connectionId);
                    _gameQueues.Add(newQueue);
                    return newQueue;
                default:
                    return null;
            }
        }
    }
}
