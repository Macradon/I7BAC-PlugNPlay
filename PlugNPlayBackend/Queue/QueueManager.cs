using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlugNPlayBackend.Queue.Interfaces;
using PlugNPlayBackend.Models.Interfaces;

namespace PlugNPlayBackend.Queue
{
    public class QueueManager : IQueueManager
    {
        private static List<IGameQueue> _gameQueues;

        public QueueManager()
        {
            _gameQueues = new List<IGameQueue>();
        }

        public int AddToQueue(string gameID, string connectionID)
        {
            var queueObj = _gameQueues.Find(queue => queue.Id.Equals(gameID));
            if (queueObj != null)
            {
                if (queueObj.GetSize() < 2)
                {
                    if (queueObj.AddToQueue(connectionID))
                    {
                        return 1;
                    }
                    return 2;
                }
                _gameQueues.Remove(queueObj);
                CreateNewQueue(gameID, connectionID);
            }
            return 0;
        }

        private void CreateNewQueue(string gameID, string connectionId)
        {
            
        }
    }
}
