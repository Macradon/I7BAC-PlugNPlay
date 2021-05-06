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
        public List<List<string>> GetFriendlist(string username)
        {
            var userObj = _userService.Get(username);
            if (userObj != null)
            { 
                List<List<string>> combinedFriendlist = new List<List<string>>();
                combinedFriendlist.Add(userObj.OfflineFriendlist);
                combinedFriendlist.Add(userObj.OnlineFriendlist);
                return combinedFriendlist;
            }
            return null;
        }

        public List<List<string>> AddFriend(string username, string friendUsername)
        {
            var userObj = _userService.Get(username);
            var friendUserObj = _userService.Get(friendUsername);
            if (userObj != null && friendUserObj != null)
            {
                List<List<string>> combinedFriendlist = new List<List<string>>();
                if (friendUserObj.ConnectionID != null)
                {
                    userObj.OnlineFriendlist.Add(friendUserObj.Username);
                    _userService.Update(userObj.Username, userObj);
                    friendUserObj.OnlineFriendlist.Add(username);
                    _userService.Update(friendUserObj.Username, friendUserObj);
                    combinedFriendlist.Add(userObj.OfflineFriendlist);
                    combinedFriendlist.Add(userObj.OnlineFriendlist);
                    return combinedFriendlist;
                }
                userObj.OfflineFriendlist.Add(friendUserObj.Username);
                _userService.Update(userObj.Username, userObj);
                friendUserObj.OnlineFriendlist.Add(userObj.Username);
                _userService.Update(friendUserObj.Username, friendUserObj);
                combinedFriendlist.Add(userObj.OfflineFriendlist);
                combinedFriendlist.Add(userObj.OnlineFriendlist);
                return combinedFriendlist;
            }
            return null;
        }

        public List<string> GetOnlineFriends(string username)
        {
            var userObj = _userService.Get(username);
            if (userObj != null)
            {
                return userObj.OnlineFriendlist;
            }
            return null;
        }

        public void UpdateOnlineStatus(string username, string status)
        {
            

        }

        public List<List<string>> RemoveFriend(string username, string friendUsername)
        {
            var userObj = _userService.Get(username);
            var friendUserObj = _userService.Get(friendUsername);
            if (userObj != null && friendUserObj != null)
            {
                List<List<string>> combinedFriendlist = new List<List<string>>();
                if (friendUserObj.ConnectionID != null)
                {
                    userObj.OnlineFriendlist.Remove(friendUserObj.Username);
                    _userService.Update(userObj.Username, userObj);
                    friendUserObj.OnlineFriendlist.Remove(userObj.Username);
                    _userService.Update(friendUserObj.Username, friendUserObj);
                    combinedFriendlist.Add(userObj.OfflineFriendlist);
                    combinedFriendlist.Add(userObj.OnlineFriendlist);
                    return combinedFriendlist;
                }
                userObj.OfflineFriendlist.Remove(friendUserObj.Username);
                _userService.Update(userObj.Username, userObj);
                friendUserObj.OnlineFriendlist.Remove(userObj.Username);
                _userService.Update(friendUserObj.Username, friendUserObj);
                combinedFriendlist.Add(userObj.OfflineFriendlist);
                combinedFriendlist.Add(userObj.OnlineFriendlist);
                return combinedFriendlist;
            }
            return null;
        }
        #endregion
    }
}
