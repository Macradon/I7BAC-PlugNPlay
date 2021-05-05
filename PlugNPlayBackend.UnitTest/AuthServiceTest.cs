using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using PlugNPlayBackend.Controllers;
using PlugNPlayBackend.Services.Interfaces;
using PlugNPlayBackend.Models;
using NSubstitute;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NSubstitute.ReturnsExtensions;
using Microsoft.AspNetCore.SignalR;
using PlugNPlayBackend.Hubs;

namespace PlugNPlayBackend.UnitTest
{
    public class AuthServiceTest
    {
        //Variables and auxiliary
        IUserService userService;
        IConfiguration configuration;
        IFriendlistService friendlistService;
        IPlugNPlayDatabaseSettings plugNPlayDatabaseSettings;
        IHubContext<GlobalHub> hub;

        public AuthServiceTest()
        {
            //Implement setup
        }

        [Fact]
        async Task passwordUpdateTest()
        {

        }
    }
}
