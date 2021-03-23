using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransportManagement.DbContexts;
using TransportManagement.Entities;
using TransportManagement.Services.ImplementServices;
using TransportManagement.Services.IServices;

namespace TransportManagement
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(opt => 
            {
                opt.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                opt.EnableEndpointRouting = false; 
            });
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddTransient<IUserServices, UserServices>();
            services.AddTransient<ILocationServices, LocationServices>();
            services.AddTransient<IRouteServices, RouteServices>();
            services.AddTransient<IVehicleBrandServices, VehicleBrandServices>();
            services.AddTransient<IVehicleServices, VehicleServices>();
            services.AddTransient<ITransInfoServices, TransInfoServices>();
            services.AddTransient<IDayJobServices, DayJobServices>();
            services.AddTransient<IRoleServices, RoleServices>();
            services.AddTransient<IFuelServices, FuelServices>();
            services.AddDbContext<TransportDbContext>(opt =>
                opt.UseSqlServer(_config.GetConnectionString("Dbconection")));
            services.AddIdentity<AppIdentityUser, AppIdentityRole>(opt =>
                                {
                                    opt.User.RequireUniqueEmail = true;
                                })
                                .AddEntityFrameworkStores<TransportDbContext>()
                                .AddDefaultTokenProviders();
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
            }
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMvc(routes =>
            {
                routes.MapAreaRoute(
                name: "areas",
                areaName: "Driver",
                template: "Driver/{controller=Home}/{action=Index}/{id?}");
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
