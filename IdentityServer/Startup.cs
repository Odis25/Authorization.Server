using IdentityServer.Data;
using IdentityServer.Interfaces;
using IdentityServer.Models;
using IdentityServer.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IdentityServer
{
    public class Startup
    {
        public IConfiguration AppConfiguration { get; }

        public Startup(IConfiguration configuration) =>
            AppConfiguration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = AppConfiguration["DbConnection"];

            services.AddDbContext<AuthDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<AuthDbContext>()
                .AddClaimsPrincipalFactory<AppUserClaimsPrincipalFactory>()
                .AddDefaultTokenProviders();

            services.AddIdentityServer()
                .AddAspNetIdentity<AppUser>()
                .AddInMemoryApiResources(Configuration.ApiResources)
                .AddInMemoryApiScopes(Configuration.ApiScopes)
                .AddInMemoryClients(Configuration.Clients)
                .AddInMemoryIdentityResources(Configuration.IdentityResources)
                .AddDeveloperSigningCredential();

            services.ConfigureApplicationCookie(config =>
            {
                config.Cookie.Name = "InventoryApp.Identity.Cookie";
                config.LoginPath = "/Auth/Login";
                config.LogoutPath = "/Auth/Logout";
            });

            services.AddTransient<IAuthService, AuthService>();

            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseStaticFiles();
            app.UseIdentityServer();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
