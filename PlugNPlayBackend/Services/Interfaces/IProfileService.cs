using System.Threading.Tasks;
using PlugNPlayBackend.Models;

namespace PlugNPlayBackend.Services.Interfaces
{
    public interface IProfileService
    {
        public Task<Profile> GetProfile(string username);
    }
}
