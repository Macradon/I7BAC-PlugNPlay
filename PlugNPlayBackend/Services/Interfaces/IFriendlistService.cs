using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlugNPlayBackend.Models;

namespace PlugNPlayBackend.Services.Interfaces
{
    public interface IFriendlistService
    {
        public List<List<string>> GetFriendlist(string username);
        public List<List<string>> AddFriend(string username, string friendUsername);
        public List<List<string>> RemoveFriend(string username, string friendUsername);
        public List<string> GetOnlineFriends(string username);
        public void UpdateOnlineStatus(string username, string status);
    }
}
