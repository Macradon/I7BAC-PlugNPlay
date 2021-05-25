using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlugNPlayBackend.Models;

namespace PlugNPlayBackend.Services.Interfaces
{
    public interface IFriendlistService
    {
        public Task<List<List<string>>> GetFriendlist(string username);
        public Task<List<string>> AddFriend(string username, string friendUsername);
        public Task<List<string>> RemoveFriend(string username, string friendUsername);
    }
}
