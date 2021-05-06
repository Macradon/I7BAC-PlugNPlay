using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlugNPlayBackend.Models.Interfaces;

namespace PlugNPlayBackend.Models.GameQueue
{
    public class NimQueue : IGameQueue
    {
        private readonly Random random = new Random();
        private List<string> _queue;

        public string Id { get; set; }
        public string QueueName { get; set; }
        public int InitializeCount { get; set; }
        public int QueueMaxSize { get; set; }

        public NimQueue()
        {
            _queue = new List<string>();
            Id = "60893b5f3665f82c430c5d35";
            QueueName = "Nim" + random.Next(0, 200).ToString() + random.Next(0, 200).ToString();
            QueueMaxSize = 2;
            InitializeCount = 0;
        }

        public bool QueueFull()
        {
            if (_queue.Count == QueueMaxSize)
            {
                return true;
            }
            return false;
        }

        public void AddToQueue(string connectionId)
        {
            _queue.Add(connectionId);
        }

        public List<string> GetParticipants()
        {
            return _queue;
        }

        public bool GameInitilization()
        {
            InitializeCount++;
            if(InitializeCount == _queue.Count)
            {
                return true;
            }
            return false;
        }
    }
}
