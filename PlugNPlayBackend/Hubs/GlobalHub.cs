using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using PlugNPlayBackend.Services;
using PlugNPlayBackend.Models;

namespace PlugNPlayBackend.Hubs
{
    public class GlobalHub : Hub
    {
        private readonly UserService _userService;
        private const string _globalChat = "GlobalChat";
        private static string[] _gameChats;

        public GlobalHub(UserService userService)
        {
            _userService = userService;
        }

        //Method to send a message to a specified room, either Global Chat or a specific game's room
        public async Task SendMessage(string user, string message, string room)
        {
            var newMessage = new TextMessage()
            {
                Sender = user,
                Message = message
            };

            switch(room)
            {
                case _globalChat:
                    await Clients.Group(_globalChat).SendAsync("ReceiveGlobalChatMessage", newMessage);
                    break;
                default:
                    await Clients.Group(room).SendAsync("ReceiveGameChatMessage", newMessage);
                    break;
            }
        }

        //On Connected to set up connection id for later use
        public override async Task OnConnectedAsync()
        {
            await Clients.Caller.SendAsync("ConnectedToGlobalHub", Context.ConnectionId);
            await Groups.AddToGroupAsync(Context.ConnectionId, _globalChat);
            await base.OnConnectedAsync();
        }

        //On disconnected to take care of leaving the hub
        public override async Task OnDisconnectedAsync(Exception e)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, _globalChat);
            await base.OnDisconnectedAsync(e);
        }

        public async Task NotifyRequest(string connectionID)
        {
            await Clients.Client(connectionID).SendAsync("FriendRequestReceived", Context.ConnectionId);
        }
    }
}
