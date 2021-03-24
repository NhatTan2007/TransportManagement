using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransportManagement.Entities;
using TransportManagement.Models.Driver;
using TransportManagement.Models.TransportInformation;
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
        private readonly ITransInfoServices _transInfoServices;

        public HomeController(IDayJobServices dayJobServices,
                                UserManager<AppIdentityUser> userManager,
                                    ITransInfoServices transInfoServices)
        {
            _dayJobServices = dayJobServices;
            _userManager = userManager;
            _transInfoServices = transInfoServices;
        }

        public IActionResult Mobile()
        {
            return View();
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

        public async Task<IActionResult> ReportAdvances(ReportMonthModel showMonth)
        {
            var user = await _userManager.GetUserAsync(User);
            //get local time at Timezone UTC 7
            DateTime localTimeUTC7 = SystemUtilites.ConvertToTimeZone(DateTime.UtcNow, "SE Asia Standard Time");
            //get timestamp 0 AM 1st day of month
            DateTime startMonth = new DateTime(localTimeUTC7.Year, localTimeUTC7.Month, 01);
            DateTime baseDateTime = new DateTime();
            if (showMonth.Month != baseDateTime)
            {
                startMonth = showMonth.Month;
            }
            DateTime endMonth = startMonth.AddMonths(1);
            double TSstartMonth = SystemUtilites.ConvertToTimeStamp(startMonth);
            double TSendMonth = SystemUtilites.ConvertToTimeStamp(endMonth);
            List<TransInfoViewModel> transports = (await _transInfoServices.GetTransportsDoneByDriver(TSstartMonth, TSendMonth, user.Id)).ToList();
            ReportAdvancesModel report = new ReportAdvancesModel();
            foreach (var tr in transports)
            {
                report.Advances += tr.AdvanceMoney;
                report.ReturnOfAdvances += tr.ReturnOfAdvances;
            }
            ViewBag.Month = startMonth;
            ViewBag.ThisMonth = new DateTime(localTimeUTC7.Year, localTimeUTC7.Month, 01).ToString("MM/yyyy");
            return View(report);
        }
    }
}
