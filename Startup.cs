using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using pet_manager.Data;
using pet_manager.Models;
using pet_manager.Services;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Microsoft.AspNetCore.Http;
using pet_manager.Repository;
using pet_manager.BackgroundJob;
using Hangfire;
using Hangfire.PostgreSql;
using pet_manager.Infrastructure;
using pet_manager.Controllers;
using ClientNotifications.ServiceExtensions;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.Razor;
using pet_manager.ActionFilters;
using pet_manager.Extensions;

namespace pet_manager
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySql(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<Owner, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // services.AddAuthentication().AddGoogle(options=>{
            //     options.ClientId="";
            //     options.ClientSecret="";
            // });

            services.AddTransient<IPetRepository, PetRepository>();
            services.AddTransient<IWatchlistRepository, WatchlistRepository>();
            services.AddTransient<IUpdatePetRepository,UpdatePetRepository>();
            services.AddTransient<INotificationRepository,NotificationRepository>();
            
            services.AddToastNotification();
            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();
            
            services.AddSignalR();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, 
                                ApplicationDbContext context, UserManager<Owner> userManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            if (env.IsDevelopment())
                {
                app.UseStaticFiles(new StaticFileOptions()
                {
                    FileProvider = new PhysicalFileProvider(
                        Path.Combine(Directory.GetCurrentDirectory(), @"node_modules")),
                    RequestPath = new PathString("/vendor")
                });
                }

            app.UseAuthentication();

            context.Database.Migrate();

            new Seeder(context,userManager).Seed().GetAwaiter().GetResult();

            app.UseSignalR(routes=>{
                routes.MapHub<SignalServer>("signalServer");
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Pet}/{action=Index}/{id?}");
            });
        }
    }
}
