using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using PlugNPlayBackend.Services;

namespace PlugNPlayBackend.Hubs
{
    public class GlobalHub : Hub
    {
        private readonly UserService _userService;
        private readonly string _globalChat = "GlobalChat";
        private static string[] _gameChats;

        public GlobalHub(UserService userService)
        {
            _userService = userService;
        }

        public async Task SendMessage(string user, string message, string room)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public override async Task OnConnectedAsync()
        {
            await Clients.Caller.SendAsync("ConnectedToGlobalHub", Context.ConnectionId);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception e)
        {
            await base.OnDisconnectedAsync(e);
        }

        public async Task NotifyRequest(string connectionID)
        {
            await Clients.Client(connectionID).SendAsync("FriendRequestReceived", Context.ConnectionId);
        }
    }
}
