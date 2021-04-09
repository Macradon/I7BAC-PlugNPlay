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

namespace PlugNPlayBackend.Services
{
    public class FriendlistService
    {
        //Variables
        private IHubContext<GlobalHub> _hub;

        //Constructor
        public FriendlistService(IHubContext<GlobalHub> hub)
        {
            _hub = hub;
        }
    }
}
