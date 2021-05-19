using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using Azure.Identity;
using PlugNPlayBackend.Queue.Interfaces;
using PlugNPlayBackend.Queue;
using PlugNPlayBackend.Models.Interfaces;

namespace PlugNPlayBackend
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Debug.WriteLine("hello from program");
            IQueueManager newQueueManager = new QueueManager();
            IGameQueue newGameQueue = await newQueueManager.AddToQueue("60893b5f3665f82c430c5d35", "123123abcd");
            Debug.WriteLine("Added player to queue: " + newGameQueue.QueueName);
            Debug.WriteLine("Queue full: " + newGameQueue.QueueFull().ToString());
            IGameQueue anotherNewGameQueue = await newQueueManager.AddToQueue("60893b5f3665f82c430c5d35", "321321dcba");
            Debug.WriteLine("Added player to queue: " + anotherNewGameQueue.QueueName);
            Debug.WriteLine("Queue full: " + anotherNewGameQueue.QueueFull().ToString());
            Debug.WriteLine("Game initilization update for room: " + newGameQueue.QueueName);
            Debug.WriteLine("Game initilization for room " + newGameQueue.QueueName + " complete: " + newGameQueue.GameInitilization().ToString());
            IGameQueue newAnotherNewGameQueue = await newQueueManager.AddToQueue("60893b5f3665f82c430c5d35", "444444ddd");
            Debug.WriteLine("Added player to queue: " + newAnotherNewGameQueue.QueueName);
            Debug.WriteLine("Queue full: " + newAnotherNewGameQueue.QueueFull().ToString());
            Debug.WriteLine("Game initilization update for room: " + anotherNewGameQueue.QueueName);
            Debug.WriteLine("Game initilization for room " + anotherNewGameQueue.QueueName + " complete: " + anotherNewGameQueue.GameInitilization().ToString());

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseKestrel()
                    .UseContentRoot(Directory.GetCurrentDirectory())
                    .UseIISIntegration()
                    .UseStartup<Startup>();
                });
    }
}
