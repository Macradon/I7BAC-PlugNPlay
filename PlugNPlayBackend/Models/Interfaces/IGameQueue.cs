using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlugNPlayBackend.Models.Interfaces
{
    public interface IGameQueue
    {
        public string Id { get; set; }
        public string Name();
        public bool AddToQueue(string connectionId);
        public int GetSize();
    }
}
