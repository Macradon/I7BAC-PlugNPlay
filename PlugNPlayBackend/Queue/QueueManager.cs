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

        }

    }
}
