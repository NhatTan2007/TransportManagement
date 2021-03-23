using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransportManagement.Entities;
using TransportManagement.Services.IServices;
using TransportManagement.Utilities;

namespace TransportManagement.Areas.Driver.Controllers
{
    [Area("Driver")]
    [Authorize(Roles = "Lái xe")]
    public class HomeController : Controller
    {
        private readonly IDayJobServices _dayJobServices;
        private readonly UserManager<AppIdentityUser> _userManager;

        public HomeController(IDayJobServices dayJobServices,
                                UserManager<AppIdentityUser> userManager)
        {
            _dayJobServices = dayJobServices;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                DateTime localTimeUTC7 = SystemUtilites.ConvertToTimeZone(DateTime.UtcNow, "SE Asia Standard Time");
                var dayJob = _dayJobServices.GetDayJob(user.Id, SystemUtilites.ConvertToTimeStamp(localTimeUTC7.Date));
                if (dayJob == null)
                {
                    dayJob = new DayJob()
                    {
                        DayJobId = Guid.NewGuid().ToString(),
                        Date = SystemUtilites.ConvertToTimeStamp(localTimeUTC7.Date),
                        DriverId = user.Id,
                        Transports = new List<TransportInformation>()
                    };
                    await _dayJobServices.Create(dayJob);
                }
                dayJob.Transports = dayJob.Transports.OrderByDescending(t => t.DateStartLocal).ToList();
                return View(dayJob);
            }
            return RedirectToAction(actionName: "Login", controllerName: "Account", new { area = "" });
        }
    }
}
