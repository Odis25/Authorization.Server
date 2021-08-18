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
using Microsoft.Extensions.Logging;
using System.Reflection;

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
                //.AddInMemoryApiResources(Configuration.ApiResources)
                //.AddInMemoryApiScopes(Configuration.ApiScopes)
                //.AddInMemoryIdentityResources(Configuration.IdentityResources)
                //.AddInMemoryClients(Configuration.Clients)
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = builder =>
                        builder.UseSqlServer(connectionString,
                        sql => sql.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name));
                })
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = builder =>
                        builder.UseSqlServer(connectionString,
                        sql => sql.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name));

                    options.EnableTokenCleanup = true;
                    options.TokenCleanupInterval = 3600;
                })
                .AddAspNetIdentity<AppUser>()
                .AddDeveloperSigningCredential();

            services.ConfigureApplicationCookie(config =>
            {
                config.Cookie.Name = "InventoryApp.Identity.Cookie";
                config.LoginPath = "/Auth/Login";
                config.LogoutPath = "/Auth/Logout";
            });

            services.AddTransient<IAuthService, AuthService>();

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.WithOrigins(
                        "https://192.168.110.17:8080",
                        "https://pnrsu-server.incomsystem.ru:8080")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });

            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Program> logger)
        {
            try
            {
                DbInitializer.InitializeDatabase(app);
            }
            catch (System.Exception ex)
            {
                logger.LogError(ex.Message);
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseStaticFiles();
            app.UseCors();

            app.UseIdentityServer();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
