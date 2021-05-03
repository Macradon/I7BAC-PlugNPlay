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
        private IHubContext<GlobalHub> _hub;

        //Constructor
        public FriendlistService(IUserService userService, IHubContext<GlobalHub> hub)
        {
            _hub = hub;
        }

        //This sections implements CRUD operations for the service 
        #region CRUD operations
        public List<string> GetFriendlist(string username)
        {
            var userObj = _userService.Get(username);
            if (userObj != null)
                return userObj.Friendlist;
            return null;
        }

        public List<string> AddFriend(string username, string friendUsername)
        {
            var userObj = _userService.Get(username);
            var friendUserObj = _userService.Get(friendUsername);
            if (userObj != null && friendUserObj != null)
            {
                userObj.Friendlist.Add(friendUsername);
                _userService.Update(userObj.Username, userObj);
                return userObj.Friendlist;
            }
            return null;
        }

        public List<string> UpdateOnlineStatus(string username, string status)
        {

            return null;
        }

        public List<string> RemoveFriend(string username, string friendUsername)
        {
            var userObj = _userService.Get(username);
            if (userObj != null)
            {
                userObj.Friendlist.Remove(friendUsername);
                _userService.Update(userObj.Username, userObj);
                return userObj.Friendlist;
            }
            return null;
        }
        #endregion
    }
}
