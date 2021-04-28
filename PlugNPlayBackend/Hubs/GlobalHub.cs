using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using PlugNPlayBackend.Services.Interfaces;
using PlugNPlayBackend.Models;
using PlugNPlayBackend.Models.Interfaces;
using PlugNPlayBackend.Models.GameQueue;
using PlugNPlayBackend.Queue.Interfaces;
using System.Security.Cryptography;

namespace PlugNPlayBackend.Hubs
{
    public class GlobalHub : Hub
    {
        private readonly IUserService _userService;
        private const string _globalChat = "GlobalChat";
        private readonly IQueueManager _queueManager;

        public GlobalHub(IUserService userService, IQueueManager queueManager)
        {
            _userService = userService;
            _queueManager = queueManager;
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
                    await Clients.Group(_globalChat).SendAsync("ReceiveGlobalChatMessage", JsonConvert.SerializeObject(newMessage));
                    break;
                default:
                    await Clients.Group(room).SendAsync("ReceiveGameChatMessage", JsonConvert.SerializeObject(newMessage));
                    break;
            }
        }

        //Method to queue up for a game
        public async Task QueueUpForGame(string gameID)
        {
            var queue = _queueManager.AddToQueue(gameID, Context.ConnectionId);
            if (queue.QueueFull)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, queue.QueueName);
                await Clients.Caller.SendAsync("QueuedUpForGame", queue.QueueName);
                await NotifyPlayers(queue.GetParticipants());
            }
            else
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, queue.QueueName);
                await Clients.Caller.SendAsync("QueuedUpForGame", queue.QueueName);
            }
        }

        private async Task NotifyPlayers(List<string> players)
        {
            foreach (string player in players)
            {
                await Clients.Client(player).SendAsync("QueueMatchFound");
            }
        }

        //Method to send a move to a specific game room
        public async Task SendMove(string move, string roomName)
        {
            await Clients.Group(roomName).SendAsync("ReceiveMove", move);
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
