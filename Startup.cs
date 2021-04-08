using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Data;
using MvcActor.Data;
using MvcSeries.Data;
using System.Threading.Tasks;
using ElectronNET.API;
using ElectronNET.API.Entities;
using System;

namespace IMDB
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            WebEnvironment = env;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment WebEnvironment { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddDbContext<MvcMovieContext>(options =>
                options.UseNpgsql(GetHerokuConnectionString()));
            services.AddDbContext<MvcActorContext>(options =>
                options.UseNpgsql(GetHerokuConnectionString()));
            services.AddDbContext<MvcSeriesContext>(options =>
                options.UseNpgsql(GetHerokuConnectionString()));
        }

        private string GetHerokuConnectionString() {
            string connectionUrl = "postgres://ldetimjpwraxvx:8dd3d702392b159b4e1503c1b86ed15ea1834349f8c0826fa8262cb214923f50@ec2-54-155-87-214.eu-west-1.compute.amazonaws.com:5432/d2jv7d6pmkrc6j";
            var databaseUri = new Uri(connectionUrl);
            string db = databaseUri.LocalPath.TrimStart('/');
            string[] userInfo = databaseUri.UserInfo.Split(':', StringSplitOptions.RemoveEmptyEntries);
            return $"User ID={userInfo[0]};Password={userInfo[1]};Host={databaseUri.Host};Port={databaseUri.Port};Database={db};Pooling=true;SSL Mode=Require;Trust Server Certificate=True;";
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
                var options = new BrowserWindowOptions{
                    Width = 1920,
                    Height = 1080
                };
                Task.Run(async () => await Electron.WindowManager.CreateWindowAsync(options));


        }
    }
}
