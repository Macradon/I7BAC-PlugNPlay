using System.Collections.Generic;
using System.Threading.Tasks;
using PlugNPlayBackend.Services.Interfaces;
using PlugNPlayBackend.Models;

namespace PlugNPlayBackend.Services
{
    public class FriendlistService : IFriendlistService
    {
        //Variables
        private IUserService _userService;

        //Constructor
        public FriendlistService(IUserService userService)
        {
            _userService = userService;
        }

        //This sections implements CRUD operations for the service 
        #region CRUD operations
        public async Task<FriendList> GetFriendlist(string username)
        {
            var userObj = await _userService.Get(username);
            if (userObj != null)
            {
                var friendList = new FriendList(userObj.FriendRequests);
                foreach(string friendUsername in userObj.Friendlist)
                {
                    var friendUser = await _userService.Get(friendUsername);
                    if(friendUser.ConnectionID != null)
                    {
                        friendList.OnlineFriends.Add(friendUsername);
                    } else
                    {
                        friendList.OfflineFriends.Add(friendUsername);
                    }
                }
                return friendList;
            }
            return null;
        }

        public async Task<List<string>> AddFriend(string username, string friendUsername)
        {
            var userObj = await _userService.Get(username);
            var friendUserObj = await _userService.Get(friendUsername);
            if (userObj != null && friendUserObj != null)
            {
                userObj.Friendlist.Add(friendUserObj.Username);
                _userService.Update(userObj.Username, userObj);
                friendUserObj.Friendlist.Add(username);
                _userService.Update(friendUserObj.Username, friendUserObj);
                return userObj.Friendlist;
            }
            return null;
        }

        public async Task SendFriendRequest(string recipientUsername, string requestingUsername)
        {
            var recipientUser = await _userService.Get(recipientUsername);
            recipientUser.FriendRequests.Add(requestingUsername);
            _userService.Update(recipientUser.Username, recipientUser);
        }

        public async Task RemoveFriendRequest(string recipientUsername, string requestingUsername)
        {
            var recipientUser = await _userService.Get(recipientUsername);
            recipientUser.FriendRequests.Remove(requestingUsername);
            _userService.Update(recipientUser.Username, recipientUser);
        }

        public async Task<List<string>> RemoveFriend(string username, string friendUsername)
        {
            var userObj = await _userService.Get(username);
            var friendUserObj = await _userService.Get(friendUsername);
            if (userObj != null && friendUserObj != null)
            {
                userObj.Friendlist.Remove(friendUserObj.Username);
                _userService.Update(userObj.Username, userObj);
                friendUserObj.Friendlist.Remove(userObj.Username);
                _userService.Update(friendUserObj.Username, friendUserObj);
                return userObj.Friendlist;
            }
            return null;
        }
        #endregion
    }
}
