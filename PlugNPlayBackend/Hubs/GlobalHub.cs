using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using PlugNPlayBackend.Services.Interfaces;
using PlugNPlayBackend.Models;
using PlugNPlayBackend.Queue.Interfaces;

namespace PlugNPlayBackend.Hubs
{
    public class GlobalHub : Hub
    {
        private readonly IUserService _userService;
        private readonly IFriendlistService _friendlistService;
        private const string _globalChat = "GlobalChat";
        private readonly IQueueManager _queueManager;

        public GlobalHub(IUserService userService, IFriendlistService friendlistService, IQueueManager queueManager)
        {
            _userService = userService;
            _friendlistService = friendlistService;
            _queueManager = queueManager;
        }

        #region Chat
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
        #endregion

        #region Game Queueing
        //Method to queue up for a game
        public async Task QueueUpForGame(string gameID)
        {
            var queue = await _queueManager.AddToQueue(gameID, Context.ConnectionId);
            if (queue != null)
            {
                if (queue.QueueFull())
                {
                    await Groups.AddToGroupAsync(Context.ConnectionId, queue.QueueName);
                    await Clients.Caller.SendAsync("QueuedUpForGame", queue.QueueName);
                    await NotifyPlayers(queue.GetParticipants(), "queue");
                }
                else
                {
                    await Groups.AddToGroupAsync(Context.ConnectionId, queue.QueueName);
                    await Clients.Caller.SendAsync("QueuedUpForGame", queue.QueueName);
                }
            }
        }

        public async Task GameInitializationComplete(string roomName)
        {
            var queue = await _queueManager.GetQueue(roomName);
            if(queue.GameInitilization())
            {
                await NotifyPlayers(queue.GetParticipants(), "start");
                _queueManager.RemoveQueue(roomName);
            }
        }

        //Helper method to notify players of a QueueMatch or GameStart
        private async Task NotifyPlayers(List<string> players, string notificationType, string roomName = "default")
        {
            switch (notificationType)
            {
                case "queue":
                    for (int i = 0; i < players.Count; i++)
                    {
                        await Clients.Client(players[i]).SendAsync("QueueMatchFound", i);
                    }
                    break;
                case "start":
                    for (int i = 0; i < players.Count; i++)
                    {
                        await Clients.Client(players[i]).SendAsync("GameStart");
                    }
                    break;
                case "request":
                    for (int i = 0; i < players.Count; i++)
                    {
                        await Clients.Client(players[i]).SendAsync("ChallengeAccepted", i, roomName);
                    }
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region Game Move
        //Method to send a move to a specific game room
        public async Task SendMove(string move, string roomName)
        {
            await Clients.Group(roomName).SendAsync("ReceiveMove", move);
        }
        #endregion

        #region Game Request
        public async Task SendgameRequest(string requestingUsername, string recipientUsername, string gameID)
        {
            var requestingUser = await _userService.Get(requestingUsername);
            var recipientUser = await _userService.Get(recipientUsername);
            if (requestingUser != null && recipientUser != null)
            {
                await Clients.Client(recipientUser.ConnectionID).SendAsync("ChallengeFrom", requestingUser.Username, gameID);
            }
        }

        public async Task ChallengeDecline(string requestingUsername)
        {
            var requestingUser = await _userService.Get(requestingUsername);
            if (requestingUser != null)
            {
                await Clients.Client(requestingUser.ConnectionID).SendAsync("ChallengeHasBeenDenied");
            }
        }

        public async Task ChallengeAccept(string requestingUsername, string gameID)
        {
            var requestingUser = await _userService.Get(requestingUsername);
            if (requestingUser != null)
            {
                var queueList = new List<string>();
                var queueName = _queueManager.CreateQueueName(gameID);
                await Groups.AddToGroupAsync(requestingUser.ConnectionID, queueName);
                queueList.Add(requestingUser.ConnectionID);
                await Groups.AddToGroupAsync(Context.ConnectionId, queueName);
                queueList.Add(Context.ConnectionId);
                await NotifyPlayers(queueList, "request", queueName);
            }
        }
        #endregion      

        #region Friendlist
        public async Task SendFriendRequest(string recipientUsername)
        {
            var requestingUser = await _userService.GetByConnection(Context.ConnectionId);
            var recipientUser = await _userService.Get(recipientUsername);
            if (requestingUser != null && recipientUser != null)
            {
                await _friendlistService.SendFriendRequest(recipientUser.Username, requestingUser.Username);
                await NotifyRequest(recipientUser.ConnectionID, requestingUser.Username);
            }
        }

        public async Task NotifyRequest(string recipientConnectionID, string requestingUsername)
        {
            await Clients.Client(recipientConnectionID).SendAsync("FriendRequest", requestingUsername);
        }

        public async Task AcceptFriendRequest(string requestingtUsername)
        {
            var requestingUser = await _userService.Get(requestingtUsername);
            var recipientUser = await _userService.GetByConnection(Context.ConnectionId);
            if (requestingUser != null && recipientUser != null)
            {
                recipientUser.Friendlist.Add(requestingtUsername);
                _userService.Update(recipientUser.Username, recipientUser);
                requestingUser.Friendlist.Add(recipientUser.Username);
                _userService.Update(requestingtUsername, requestingUser);

                if (requestingUser.ConnectionID != null)
                {
                    await Clients.Client(requestingUser.ConnectionID).SendAsync("FriendRequestAccepted");
                }
            }
        }

        public async Task DeclineFriendRequest(string requestingUsername)
        {
            var recipientUser = await _userService.Get(Context.ConnectionId);
            recipientUser.FriendRequests.Remove(requestingUsername);
            _userService.Update(recipientUser.Username, recipientUser);
        }

        public async Task NotifyOnLogin(string username)
        {
            var userObj = await _userService.Get(username);
            if (userObj != null)
            {
                userObj.ConnectionID = Context.ConnectionId;
                _userService.Update(userObj.Username, userObj);

                foreach (string onlineUser in userObj.Friendlist)
                {
                    var onlineFriend = await _userService.Get(onlineUser);
                    if (onlineFriend != null)
                    {
                        if (onlineFriend.ConnectionID != null)
                        {
                            await Clients.Client(onlineFriend.ConnectionID).SendAsync("FriendOnline", userObj.Username);
                        }
                    }
                }
            }
        }

        private async Task NotifyOnLogoff(string connectionID)
        {
            var userObj = await _userService.GetByConnection(connectionID);
            if (userObj != null)
            {
                userObj.ConnectionID = null;
                _userService.Update(userObj.Username, userObj);
                var friendList = userObj.Friendlist;

                foreach (string onlineUser in friendList)
                {
                    var onlineFriend = await _userService.Get(onlineUser);
                    if (onlineFriend != null)
                    {
                        if (onlineFriend.ConnectionID != null)
                        {
                            await Clients.Client(onlineFriend.ConnectionID).SendAsync("FriendOffline", userObj.Username);
                        }
                    }
                }
            }
        }
        #endregion

        #region Override methods
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
            await NotifyOnLogoff(Context.ConnectionId);
            await base.OnDisconnectedAsync(e);
        }
        #endregion
    }
}
