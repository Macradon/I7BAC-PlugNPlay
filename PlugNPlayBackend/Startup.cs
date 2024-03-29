using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using PlugNPlayBackend.Models;
using PlugNPlayBackend.Services;
using PlugNPlayBackend.Services.Interfaces;
using PlugNPlayBackend.Hubs;
using PlugNPlayBackend.Queue.Interfaces;
using PlugNPlayBackend.Queue;

namespace PlugNPlayBackend
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Initializes cross origin resoruce sharing for local hosts in http and https on port 4200
            services.AddCors(options =>
            {
                options.AddPolicy("PolicyCORS",
                                  builder =>
                                  {
                                      /*builder.WithOrigins("https://localhost:4200",
                                                          "http://localhost:4200")
                                                          .AllowAnyHeader()
                                                          .AllowAnyMethod()
                                                          .AllowCredentials();*/
                                      builder.AllowAnyOrigin();
                                  });

            });

            //Initializes database settings
            services.Configure<PlugNPlayDatabaseSettings>(
                Configuration.GetSection(nameof(PlugNPlayDatabaseSettings)));

            //Initializes database connection
            services.AddSingleton<IPlugNPlayDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<PlugNPlayDatabaseSettings>>().Value);

            //Initializes SignalR hubs
            services.AddSignalR();

            //Initializes services
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<IFriendlistService, FriendlistService>();
            services.AddSingleton<IProfileService, ProfileService>();
            services.AddSingleton<IAuthService, AuthService>();
            services.AddSingleton<IGamestatService, GameStatService>();
            services.AddSingleton<IGameService, GameService>();

            //Initializes QueueManager
            services.AddSingleton<IQueueManager, QueueManager>();

            //Initializes controllers
            services.AddControllers().AddNewtonsoftJson(options => options.UseMemberCasing()); ;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //Developer environment check
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //User initialization
            app.UseCors("PolicyCORS");

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<GlobalHub>("/globalHub");
            });
        }
    }
}
