using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlugNPlayBackend.Models
{
    public class FriendList
    {
        public FriendList(List<string> friendRequests)
        {
            OnlineFriends = new List<string>();
            OfflineFriends = new List<string>();
            FriendRequests = friendRequests;
        }

        public List<string> OnlineFriends { get; set; }
        public List<string> OfflineFriends { get; set; }
        public List<string> FriendRequests { get; set; }
    }
}
