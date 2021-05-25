using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlugNPlayBackend.Models
{
    public class FriendList
    {
        public FriendList()
        {
            OnlineFriends = new List<string>();
            OfflineFriends = new List<string>();
        }

        public List<string> OnlineFriends { get; set; }
        public List<string> OfflineFriends { get; set; }
    }
}
