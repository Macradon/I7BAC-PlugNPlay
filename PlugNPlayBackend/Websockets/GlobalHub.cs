using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlugNPlayBackend.Services;
using Microsoft.AspNetCore.SignalR;

namespace PlugNPlayBackend.Websockets
{
    public class GlobalHub : Hub
    {
        public static GlobalHub instance;
        public UserService _userService;

        public GlobalHub(UserService userService)
        {
            _userService = userService;
        }
    }
}
