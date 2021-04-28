using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlugNPlayBackend.Models;

namespace PlugNPlayBackend.Services.Interfaces
{
    public interface IAuthService
    {
        public User PasswordUpdate(string username, string password);
        public Token Login(string username, string password);
        public bool Register(string username, string password, string email);
        public bool CheckUserExistance(string username);
        public bool PasswordCheck(string username, string password);
        public Token GenerateToken(string username);
    }
}
