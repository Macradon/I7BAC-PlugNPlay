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
        public List<string> Get(string username)
        {
            var userObj = _userService.Get(username);
            if (userObj == null)
                return null;
            return userObj.Friendlist;
        }

        public List<string> Add(string username, string friendUsername)
        {
            var userObj = _userService.Get(username);
            var friendUserObj = _userService.Get(friendUsername);
            if (userObj == null)
            {
                return null;
            }
            if (friendUsername == null)
            {
                return null;
            }
            userObj.Friendlist.Add(friendUsername);
            _userService.Update(username, userObj);
            return userObj.Friendlist;
        }
    }
}
