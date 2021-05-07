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
        private readonly Random random = new Random();

        public QueueManager()
        {
            _gameQueues = new List<IGameQueue>();
        }

        public IGameQueue AddToQueue(string gameID, string connectionID)
        {
            var gameQueue = _gameQueues.Find(queue => queue.Id.Equals(gameID));
            if (gameQueue != null)
            {
                if (gameQueue.QueueFull())
                {
                    return CreateNewQueue(gameID, connectionID);
                } else
                {
                    gameQueue.AddToQueue(connectionID);
                    return gameQueue;
                }
            } else
            {
                return CreateNewQueue(gameID, connectionID);
            }
        }

        public IGameQueue GetQueue(string roomName)
        {
            var queue = _gameQueues.Find(queue => queue.QueueName.Equals(roomName));
            if (queue != null)
            {
                return queue;
            }
            return null;
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

        public void RemoveQueue(string queueName)
        {
            var queue = _gameQueues.Find(queue => queue.QueueName.Equals(queueName));
            _gameQueues.Remove(queue);
        }

        public string CreateQueueName(string gameID)
        {
            return gameID + random.Next(0,200).ToString() + random.Next(0, 200).ToString();
        }
    }
}
