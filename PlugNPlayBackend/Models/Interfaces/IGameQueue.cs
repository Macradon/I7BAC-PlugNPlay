using System.Collections.Generic;

namespace PlugNPlayBackend.Models.Interfaces
{
    public interface IGameQueue
    {
        public string Id { get; set; }
        public string QueueName { get; set; }
        public int InitializeCount { get; set; }
        public int QueueMaxSize { get; set; }
        public bool GameInitilization();
        public bool QueueFull();
        public void AddToQueue(string connectionId);
        public List<string> GetParticipants();
    }
}
