using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlugNPlayBackend.Models;

namespace PlugNPlayBackend.Services.Interfaces
{
    public interface IFriendlistService
    {
        public Task<FriendList> GetFriendlist(string username);
        public Task<List<string>> AddFriend(string username, string friendUsername);
        public Task<List<string>> RemoveFriend(string username, string friendUsername);
        public Task SendRequest(string requestingUsername, string recipientUsername);
        public Task AcceptRequest(string requestingUsername, string recipientUsername);
        public Task DeclineRequest(string requestingUsername, string recipientUsername);
    }
}
