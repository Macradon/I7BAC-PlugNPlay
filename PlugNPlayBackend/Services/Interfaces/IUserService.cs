using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlugNPlayBackend.Models;

namespace PlugNPlayBackend.Services.Interfaces
{
    public interface IUserService
    {
        public User Get(string username);
        public User GetByConnection(string connectionID);
        public User Create(User userObj);
        public void Update(string username, User userObj);
        public void Remove(string username);
    }
}
