using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using PlugNPlayBackend.Models;
using PlugNPlayBackend.Services;
using PlugNPlayBackend.Hubs;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using PlugNPlayBackend.Services.Interfaces;

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
            var friendList = new FriendList();
            if (userObj != null)
            {
                foreach (string friend in userObj.Friendlist)
                {
                    var friendObj = await _userService.Get(friend);
                    if (friendObj.ConnectionID!=null)
                    {
                        friendList.OnlineFriends.Add(friend);
                    } else
                    {
                        friendList.OfflineFriends.Add(friend);
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

        public async Task<List<string>> GetOnlineFriends(string username)
        {
            var userObj = await _userService.Get(username);
            if (userObj != null)
            {
                return userObj.Friendlist;
            }
            return null;
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
