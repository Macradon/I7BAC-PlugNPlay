using System.Threading.Tasks;
using PlugNPlayBackend.Models;

namespace PlugNPlayBackend.Services.Interfaces
{
    public interface IAuthService
    {
        public Task<User> PasswordUpdate(string username, string password);
        public Task<Token> Login(string username, string password);
        public Task<bool> Register(string username, string password, string email);
        public Task<bool> CheckUserExistance(string username);
        public Task<bool> PasswordCheck(string username, string password);
        public Token GenerateToken(string username);
    }
}
