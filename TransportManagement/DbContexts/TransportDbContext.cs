using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransportManagement.Entities;

namespace TransportManagement.DbContexts
{
    public class TransportDbContext : IdentityDbContext<AppIdentityUser, AppIdentityRole, string>
    {
        public TransportDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Location> Locations { get; set; }
        public DbSet<VehicleBrand> VehicleBrands { get; set; }
        public DbSet<RouteInformation> RouteInformations { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<DayJob> DayJobs { get; set; }
        public DbSet<DayJob> TransportInformations { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
