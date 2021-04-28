using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlugNPlayBackend.Queue.Interfaces
{
    public interface IQueueManager
    {
        public int AddToQueue(string gameID, string connectionID);
    }
}
