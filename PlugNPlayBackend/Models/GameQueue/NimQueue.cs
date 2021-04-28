using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlugNPlayBackend.Models.Interfaces;

namespace PlugNPlayBackend.Models.GameQueue
{
    public class NimQueue : IGameQueue
    {
        private List<string> _queue;

        public NimQueue()
        {
            _queue = new List<string>();
            this.Id = "60893b5f3665f82c430c5d35";
        }

        public bool AddToQueue(string connectionId)
        {
            _queue.Add(connectionId);
            switch (_queue.Count)
            {
                case 0:
                    _queue.Add(connectionId);
                    Player1 = connectionId;
                    break;
                case 1:
                    _queue.Add(connectionId);
                    Player2 = connectionId;
                    return true;
                default:
                    break;
            }
            return false;
        }

        public int GetSize()
        {
            return _queue.Count;
        }

        public string Id { get; set; }

        public string Name()
        {
            return "Nim";
        }

        public string Player1 { get; set; }
        public string Player2 { get; set; }
    }
}
