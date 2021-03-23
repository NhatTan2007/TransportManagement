﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TransportManagement.DbContexts;

namespace TransportManagement.Migrations
{
    [DbContext(typeof(TransportDbContext))]
    [Migration("20210323033134_Add_Fuels_Table")]
    partial class Add_Fuels_Table
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");

                    b.HasData(
                        new
                        {
                            UserId = "84572bc3-25fc-4ef8-9056-67c4da04069b",
                            RoleId = "8dd36636-b4d8-4010-8594-caebfbe55991"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("TransportManagement.Entities.AppIdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<byte>("RolePriority")
                        .HasColumnType("tinyint");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");

                    b.HasData(
                        new
                        {
                            Id = "8dd36636-b4d8-4010-8594-caebfbe55991",
                            ConcurrencyStamp = "5abef0a2-8e29-4e00-96c3-10757f2babbc",
                            IsActive = true,
                            Name = "Quản trị viên hệ thống",
                            NormalizedName = "QUẢN TRỊ VIÊN HỆ THỐNG",
                            RolePriority = (byte)1
                        });
                });

            modelBuilder.Entity("TransportManagement.Entities.AppIdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Avatar")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Department")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("bit");

                    b.Property<string>("JobTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("MiddleName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<int>("RolePriority")
                        .HasColumnType("int");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");

                    b.HasData(
                        new
                        {
                            Id = "84572bc3-25fc-4ef8-9056-67c4da04069b",
                            AccessFailedCount = 0,
                            Avatar = "noavatar.png",
                            ConcurrencyStamp = "a11d0a3c-45df-463c-b5b9-e04cb0a70583",
                            EmailConfirmed = false,
                            FirstName = "Administrator",
                            IsActive = true,
                            IsAvailable = true,
                            LastName = "System",
                            LockoutEnabled = false,
                            NormalizedUserName = "ADMIN",
                            PasswordHash = "AQAAAAEAACcQAAAAEMAQJcrq0MAAR/+TXLoacDQ4OiPk8BRyU9s71T1P28Bw74bUYOU+Wk7WBdzfuuRTPA==",
                            PhoneNumber = "0911345992",
                            PhoneNumberConfirmed = true,
                            RolePriority = 0,
                            SecurityStamp = "d1f1401c-602a-4820-8531-34b62b2368c3",
                            TwoFactorEnabled = false,
                            UserName = "admin"
                        });
                });

            modelBuilder.Entity("TransportManagement.Entities.DayJob", b =>
                {
                    b.Property<string>("DayJobId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<double>("Date")
                        .HasColumnType("float");

                    b.Property<string>("DriverId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("DayJobId");

                    b.HasIndex("DriverId");

                    b.ToTable("DayJobs");
                });

            modelBuilder.Entity("TransportManagement.Entities.EditTransportInformation", b =>
                {
                    b.Property<string>("EditId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<double>("DateEditLocal")
                        .HasColumnType("float");

                    b.Property<double>("DateEditUTC")
                        .HasColumnType("float");

                    b.Property<string>("EditContent")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)");

                    b.Property<string>("TimeZone")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("TransportId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("UserEditId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("EditId");

                    b.HasIndex("TransportId");

                    b.HasIndex("UserEditId");

                    b.ToTable("EditTransportInformations");
                });

            modelBuilder.Entity("TransportManagement.Entities.Fuel", b =>
                {
                    b.Property<int>("FuelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FuelName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<decimal>("FuelPrice")
                        .HasColumnType("decimal(18,0)");

                    b.HasKey("FuelId");

                    b.ToTable("Fuels");
                });

            modelBuilder.Entity("TransportManagement.Entities.Location", b =>
                {
                    b.Property<string>("LocationId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LocationName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("LocationId");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("TransportManagement.Entities.RouteInformation", b =>
                {
                    b.Property<string>("RouteId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ArrivalPlace")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("ArrivalPlaceId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("DeparturePlace")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("DeparturePlaceId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Distance")
                        .HasColumnType("int");

                    b.HasKey("RouteId");

                    b.HasIndex("ArrivalPlaceId");

                    b.HasIndex("DeparturePlaceId");

                    b.ToTable("RouteInformations");
                });

            modelBuilder.Entity("TransportManagement.Entities.TransportInformation", b =>
                {
                    b.Property<string>("TransportId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("AdvanceMoney")
                        .HasColumnType("decimal(18,0)");

                    b.Property<decimal>("CargoTonnage")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("CargoTypes")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<double>("DateCompletedLocal")
                        .HasColumnType("float");

                    b.Property<double>("DateCompletedUTC")
                        .HasColumnType("float");

                    b.Property<double>("DateStartLocal")
                        .HasColumnType("float");

                    b.Property<double>("DateStartUTC")
                        .HasColumnType("float");

                    b.Property<string>("DayJobId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("IsCancel")
                        .HasColumnType("bit");

                    b.Property<bool>("IsCompleted")
                        .HasColumnType("bit");

                    b.Property<string>("Note")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("ReasonCancel")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<decimal>("ReturnOfAdvances")
                        .HasColumnType("decimal(18,0)");

                    b.Property<string>("RouteId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("TimeZone")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("UserCreateId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("VehicleId")
                        .HasColumnType("int");

                    b.HasKey("TransportId");

                    b.HasIndex("DayJobId");

                    b.HasIndex("RouteId");

                    b.HasIndex("UserCreateId");

                    b.HasIndex("VehicleId");

                    b.ToTable("TransportInformations");
                });

            modelBuilder.Entity("TransportManagement.Entities.Vehicle", b =>
                {
                    b.Property<int>("VehicleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("FuelConsumptionPerTone")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("FuelId")
                        .HasColumnType("int");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasMaxLength(10)
                        .HasColumnType("bit");

                    b.Property<bool>("IsInUse")
                        .HasColumnType("bit");

                    b.Property<string>("LicensePlate")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Specifications")
                        .HasMaxLength(1500)
                        .HasColumnType("nvarchar(1500)");

                    b.Property<string>("VehicleBrandId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("VehicleName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<decimal>("VehiclePayload")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("VehicleId");

                    b.HasIndex("LicensePlate")
                        .IsUnique();

                    b.HasIndex("VehicleBrandId");

                    b.ToTable("Vehicles");
                });

            modelBuilder.Entity("TransportManagement.Entities.VehicleBrand", b =>
                {
                    b.Property<string>("BrandId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("BrandName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("BrandId");

                    b.ToTable("VehicleBrands");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("TransportManagement.Entities.AppIdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("TransportManagement.Entities.AppIdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("TransportManagement.Entities.AppIdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("TransportManagement.Entities.AppIdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TransportManagement.Entities.AppIdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("TransportManagement.Entities.AppIdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TransportManagement.Entities.DayJob", b =>
                {
                    b.HasOne("TransportManagement.Entities.AppIdentityUser", "Driver")
                        .WithMany("DayJobs")
                        .HasForeignKey("DriverId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Driver");
                });

            modelBuilder.Entity("TransportManagement.Entities.EditTransportInformation", b =>
                {
                    b.HasOne("TransportManagement.Entities.TransportInformation", "TransportInfo")
                        .WithMany("ListEdit")
                        .HasForeignKey("TransportId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("TransportManagement.Entities.AppIdentityUser", "UserEdit")
                        .WithMany("ListEdit")
                        .HasForeignKey("UserEditId");

                    b.Navigation("TransportInfo");

                    b.Navigation("UserEdit");
                });

            modelBuilder.Entity("TransportManagement.Entities.RouteInformation", b =>
                {
                    b.HasOne("TransportManagement.Entities.Location", "Arrival")
                        .WithMany()
                        .HasForeignKey("ArrivalPlaceId");

                    b.HasOne("TransportManagement.Entities.Location", "Departure")
                        .WithMany()
                        .HasForeignKey("DeparturePlaceId");

                    b.Navigation("Arrival");

                    b.Navigation("Departure");
                });

            modelBuilder.Entity("TransportManagement.Entities.TransportInformation", b =>
                {
                    b.HasOne("TransportManagement.Entities.DayJob", "DayJob")
                        .WithMany("Transports")
                        .HasForeignKey("DayJobId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TransportManagement.Entities.RouteInformation", "Route")
                        .WithMany("Transports")
                        .HasForeignKey("RouteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TransportManagement.Entities.AppIdentityUser", "UserCreate")
                        .WithMany()
                        .HasForeignKey("UserCreateId");

                    b.HasOne("TransportManagement.Entities.Vehicle", "Vehicle")
                        .WithMany("Transports")
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DayJob");

                    b.Navigation("Route");

                    b.Navigation("UserCreate");

                    b.Navigation("Vehicle");
                });

            modelBuilder.Entity("TransportManagement.Entities.Vehicle", b =>
                {
                    b.HasOne("TransportManagement.Entities.VehicleBrand", "Brand")
                        .WithMany("Vehicles")
                        .HasForeignKey("VehicleBrandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Brand");
                });

            modelBuilder.Entity("TransportManagement.Entities.AppIdentityUser", b =>
                {
                    b.Navigation("DayJobs");

                    b.Navigation("ListEdit");
                });

            modelBuilder.Entity("TransportManagement.Entities.DayJob", b =>
                {
                    b.Navigation("Transports");
                });

            modelBuilder.Entity("TransportManagement.Entities.RouteInformation", b =>
                {
                    b.Navigation("Transports");
                });

            modelBuilder.Entity("TransportManagement.Entities.TransportInformation", b =>
                {
                    b.Navigation("ListEdit");
                });

            modelBuilder.Entity("TransportManagement.Entities.Vehicle", b =>
                {
                    b.Navigation("Transports");
                });

            modelBuilder.Entity("TransportManagement.Entities.VehicleBrand", b =>
                {
                    b.Navigation("Vehicles");
                });
#pragma warning restore 612, 618
        }
    }
}
