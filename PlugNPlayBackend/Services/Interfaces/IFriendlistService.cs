using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlugNPlayBackend.Models;

namespace PlugNPlayBackend.Services.Interfaces
{
    public interface IFriendlistService
    {
        public List<string> GetFriendlist(string username);
        public List<string> AddFriend(string username, string friendUsername);
        public List<string> RemoveFriend(string username, string friendUsername);
    }
}
