using System.Threading.Tasks;
using PlugNPlayBackend.Services.Interfaces;
using PlugNPlayBackend.Models;

namespace PlugNPlayBackend.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IUserService _userService;

        //Constructor
        public ProfileService(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<Profile> GetProfile(string username)
        {
            var userObj = await _userService.Get(username);
            if (userObj != null)
            {
                var userProfile = new Profile()
                {
                    Username = userObj.Username,
                    Email = userObj.Email
                };
                return userProfile;
            }
            return null;
        }
    }
}

