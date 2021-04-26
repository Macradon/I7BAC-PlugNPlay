﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using PlugNPlayBackend.Services.Interfaces;
using PlugNPlayBackend.Models;
using PlugNPlayBackend.Models.Interfaces;
using PlugNPlayBackend.Models.GameQueue;

namespace PlugNPlayBackend.Hubs
{
    public class GlobalHub : Hub
    {
        private readonly IUserService _userService;
        private const string _globalChat = "GlobalChat";
        private static IGameQueue[] _gameQueues;
        private static int _queueSize;

        public GlobalHub(IUserService userService)
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
            string roomName = gameID + _gameQueues.Length.ToString() + _gameQueues.Length.ToString();
            _ = Groups.AddToGroupAsync(Context.ConnectionId, roomName);
            await Clients.Caller.SendAsync("QueuedForGame", roomName);
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
