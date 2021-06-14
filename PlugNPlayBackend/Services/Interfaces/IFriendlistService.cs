using System.Collections.Generic;
using System.Threading.Tasks;
using PlugNPlayBackend.Models;

namespace PlugNPlayBackend.Services.Interfaces
{
    public interface IFriendlistService
    {
        public Task<FriendList> GetFriendlist(string username);
        public Task<List<string>> AddFriend(string username, string friendUsername);
        public Task SendFriendRequest(string recipientUsername, string requestingUsername);
        public Task RemoveFriendRequest(string recipientUsername, string requestingUsername);
        public Task<List<string>> RemoveFriend(string username, string friendUsername);
    }
}
