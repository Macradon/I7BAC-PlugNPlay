using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PlugNPlayBackend.Models;
using PlugNPlayBackend.Services;

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
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                                  builder =>
                                  {
                                      builder.WithOrigins("https://localhost:4200/",
                                                          "http://localhost:4200")
                                                          .AllowAnyHeader()
                                                          .AllowAnyMethod();
                                  });
            });

            services.Configure<PlugNPlayDatabaseSettings>(
                Configuration.GetSection(nameof(PlugNPlayDatabaseSettings)));

            services.AddSingleton<IPlugNPlayDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<PlugNPlayDatabaseSettings>>().Value);

            services.AddSignalR();

            services.AddSingleton<UserService>();
            services.AddSingleton<FriendlistService>();
            services.AddSingleton<AuthService>();

            services.AddControllers().AddNewtonsoftJson(options => options.UseMemberCasing()); ;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
