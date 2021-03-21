using Microsoft.AspNetCore.Identity;
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
        public DbSet<TransportInformation> TransportInformations { get; set; }
        public DbSet<EditTransportInformation> EditTransportInformations { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Vehicle>()
                    .HasIndex(v => v.LicensePlate)
                    .IsUnique();
            //builder.Entity<EditTransportInformation>()
            //        .HasOne(t => t.TransportInfo)
            //        .WithMany(e => e.ListEdit)
            //        .OnDelete(DeleteBehavior.SetNull);

            //seed user administrator account
            builder.Entity<AppIdentityRole>().HasData(new List<AppIdentityRole>
            {
                new AppIdentityRole()
                {
                    Id = "8dd36636-b4d8-4010-8594-caebfbe55991",
                    Name = "Quản trị viên hệ thống",
                    NormalizedName = "QUẢN TRỊ VIÊN HỆ THỐNG",
                    IsActive = true,
                    RolePriority = 1
                }
            });

            var hasher = new PasswordHasher<AppIdentityUser>();

            builder.Entity<AppIdentityUser>().HasData(
                new AppIdentityUser
                {
                    Id = "84572bc3-25fc-4ef8-9056-67c4da04069b",
                    UserName = "admin",
                    NormalizedUserName = "ADMIN",
                    FirstName = "Administrator",
                    LastName = "System",
                    Avatar = "noavatar.png",
                    IsActive = true,
                    PasswordHash = hasher.HashPassword(null, "Lovelykid1@"),
                    IsAvailable = true,
                    PhoneNumber = "0911345992",
                    PhoneNumberConfirmed = true
                });

            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                    {
                        RoleId = "8dd36636-b4d8-4010-8594-caebfbe55991",
                        UserId = "84572bc3-25fc-4ef8-9056-67c4da04069b"
                }
                );
        }
    }
}
