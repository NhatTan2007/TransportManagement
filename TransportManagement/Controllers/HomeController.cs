using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransportManagement.Entities;
using TransportManagement.Models.Home;
using TransportManagement.Services.IServices;
using TransportManagement.Utilities;

namespace TransportManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly SignInManager<AppIdentityUser> _signInManager;
        private readonly UserManager<AppIdentityUser> _userManager;
        private readonly IUserServices _userServices;
        private readonly ITransInfoServices _transInfoServices;
        private readonly IVehicleServices _vehicleServices;

        public HomeController(UserManager<AppIdentityUser> userManager,
                                SignInManager<AppIdentityUser> signInManager,
                                IUserServices userServices,
                                ITransInfoServices transInfoServices,
                                IVehicleServices vehicleServices)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _userServices = userServices;
            _transInfoServices = transInfoServices;
            _vehicleServices = vehicleServices;
        }
        public async Task<IActionResult> Index()
        {
            DateTime localTimeUTC7 = SystemUtilites.ConvertToTimeZone(DateTime.UtcNow, "SE Asia Standard Time");
            DateTime monthTimeUTC7 = new DateTime(localTimeUTC7.Year, localTimeUTC7.Month, 01);
            double TSlocalTimeUTC7 = SystemUtilites.ConvertToTimeStamp(localTimeUTC7);
            double TSmonthTimeUTC7 = SystemUtilites.ConvertToTimeStamp(monthTimeUTC7);
            var transports = await _transInfoServices.GetTransports(TSmonthTimeUTC7, TSlocalTimeUTC7, "");
            HomeViewModel model = new HomeViewModel()
            {
                DriversCount = _userServices.GetDriverActiveUsers().Count,
                DriverBusyCount = _userServices.GetDriverActiveBusyUsers().Count,
                VehiclesCount = (await _vehicleServices.GetAllNotDeletedVehicles()).Count,
                VehiclesBusyCount = (await _vehicleServices.GetInUseVehicles()).Count,
                TransportInMonth = transports.Count,
                TransportCompeletedInMonth = (await _transInfoServices.GetTransportsCompeleted(TSmonthTimeUTC7, TSlocalTimeUTC7, "")).Count,
            };
            foreach (var tr in transports)
            {
                model.AdvancesInMonth += tr.AdvanceMoney;
            }
            var user = await _userManager.GetUserAsync(User);
            if (user != null && await _userManager.IsInRoleAsync(user, "Lái xe"))
            {
                return RedirectToAction(actionName: "Index", controllerName: "Home", new {area = "Driver"});
            }
            if (user == null)
            {
                return RedirectToAction(actionName: "Login", controllerName: "Account");
            }
            return View(model);
        }
    }
}
