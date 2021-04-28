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
        public bool QueueFull { get; set; }

        public NimQueue()
        {
            _queue = new List<string>();
            Id = "60893b5f3665f82c430c5d35";
            QueueName = "Nim" + random.Next(0, 200).ToString() + random.Next(0, 200).ToString();
            QueueFull = false;
        }

        public bool AddToQueue(string connectionId)
        {
            _queue.Add(connectionId);
            switch (_queue.Count)
            {
                case 0:
                    _queue.Add(connectionId);
                    return false;
                case 1:
                    _queue.Add(connectionId);
                    return true;
                default:
                    break;
            }
            return false;
        }

        public List<string> GetParticipants()
        {
            return _queue;
        }
    }
}
