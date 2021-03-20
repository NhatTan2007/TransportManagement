using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using TransportManagement.Entities;
using TransportManagement.Models.Pagination;
using TransportManagement.Models.TransportInformation;
using TransportManagement.Services;
using TransportManagement.Services.IServices;
using TransportManagement.Utilities;

namespace TransportManagement.Controllers
{
    public class TransInfoController : Controller
    {
        private readonly IUserServices _userServices;
        private readonly IRouteServices _routeServices;
        private readonly IVehicleServices _vehicleServices;
        private readonly ITransInfoServices _transInfoServices;
        private readonly IDayJobServices _dayJobServices;
        private readonly UserManager<AppIdentityUser> _userManager;

        public TransInfoController(IUserServices userServices,
                                    IVehicleServices vehicleServices,
                                    IRouteServices routeServices,
                                    ITransInfoServices transInfoServices,
                                    IDayJobServices dayJobServices,
                                    UserManager<AppIdentityUser> userManager)
        {
            _userServices = userServices;
            _routeServices = routeServices;
            _vehicleServices = vehicleServices;
            _transInfoServices = transInfoServices;
            _dayJobServices = dayJobServices;
            _userManager = userManager;
        }

        public IActionResult Manage(int page, int pageSize, string search, string timeShow = "today")
        {
            //get local time at Timezone UTC 7
            DateTime localTimeUTC7 = SystemUtilites.ConvertToTimeZone(DateTime.UtcNow, "SE Asia Standard Time");
            //get timestamp of day at 0 AM
            double TStodayUTC7 = SystemUtilites.ConvertToTimeStamp(localTimeUTC7.Date);
            //get timestamp 0 AM 1st day of month
            DateTime startMonth = new DateTime(localTimeUTC7.Year, localTimeUTC7.Month, 01);
            double TSMonthUTC7 = SystemUtilites.ConvertToTimeStamp(startMonth);
            
            PaginationViewModel<TransInfoViewModel> model = new PaginationViewModel<TransInfoViewModel>();
            if (page == 0) page = 1;
            if (pageSize == 0) pageSize = model.PageSizeItem.Min();
            if (String.IsNullOrEmpty(search))
            {
                if (timeShow == "today")
                {
                    model.Items = _transInfoServices.GetTransportsToday(TStodayUTC7, page, pageSize);
                }
                if (timeShow == "month")
                {
                    model.Items = _transInfoServices.GetTransportsToday(TSMonthUTC7, page, pageSize);
                }
            }
            else
            {
                if (timeShow == "today")
                {
                    model.Items = _transInfoServices.GetTransportsToday(TStodayUTC7, page, pageSize, search);
                }
                if (timeShow == "month")
                {
                    model.Items = _transInfoServices.GetTransportsToday(TSMonthUTC7, page, pageSize, search);
                }
            }
            int countItems = 0;
            if (model.Items != null)
            {
                if (model.Items.Any())
                {
                    countItems = model.Items.Count();
                }
            }

            model.Items = model.Items.Skip((page - 1) * pageSize).Take(pageSize);
            model.Pager = new Pager(countItems, page, pageSize);
            ViewBag.Search = search;
            return View(model);
        }
        public IActionResult Details(string transportId)
        {

            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            CreateTransInfoViewModel newTrans = new CreateTransInfoViewModel()
            {
                Drivers = _userServices.GetAvailableUsers().ToList(),
                Routes = _routeServices.GetAllRoutes().ToList(),
                Vehicles = _vehicleServices.GetNotUseVehicles().ToList()
            };
            return View(newTrans);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateTransInfoViewModel model)
        {
            //get local time at Timezone UTC 7
            DateTime localTimeUTC7 = SystemUtilites.ConvertToTimeZone(DateTime.UtcNow, "SE Asia Standard Time");
            //get timestamp of day at 0 AM
            double TStodayUTC7At0Am = SystemUtilites.ConvertToTimeStamp(localTimeUTC7.Date);
            //get timestamp now at utc
            double TSUTCNow = SystemUtilites.ConvertToTimeStamp(DateTime.UtcNow);
            //get data for select elements
            model.Drivers = _userServices.GetAvailableUsers().ToList();
            model.Routes = _routeServices.GetAllRoutes().ToList();
            model.Vehicles = _vehicleServices.GetNotUseVehicles().ToList();
            string message = String.Empty;
            if (ModelState.IsValid)
            {
                //check the vehicle is used
                string driverIdUseVehicle = _vehicleServices.IsVehicleInUsedByAnotherDriver(model.DriverId, model.VehicleId, TStodayUTC7At0Am);
                if (!String.IsNullOrEmpty(driverIdUseVehicle))
                {
                    var driverUseVehicle = _userServices.GetUser(driverIdUseVehicle);
                    message = $"Xe đang được sử dụng bởi {driverUseVehicle.FullName}";
                    TempData["UserMessage"] = SystemUtilites.SendSystemNotification(NotificationType.Error, message);
                    return View(model);
                }

                //create new TransportInformation
                var user = await _userManager.GetUserAsync(User);
                TransportInformation newTrans = new TransportInformation()
                {
                    TransportId = Guid.NewGuid().ToString(),
                    AdvanceMoney = model.AdvanceMoney,
                    DateStartUTC = TSUTCNow,
                    DateStartLocal = SystemUtilites.ConvertToTimeStamp(localTimeUTC7),
                    TimeZone = "SE Asia Standard Time",
                    CargoTypes = model.CargoTypes,
                    Note = model.Note,
                    VehicleId = model.VehicleId,
                    RouteId = model.RouteId,
                    UserCreateId = user.Id
                };
                //get or create if not dayjob has date match today timeStamp
                DayJob driverDayJob = _dayJobServices.GetDayJob(model.DriverId, TStodayUTC7At0Am);
                if (driverDayJob == null)
                {
                    driverDayJob = new DayJob()
                    {
                        DayJobId = Guid.NewGuid().ToString(),
                        DriverId = model.DriverId,
                        Date = TStodayUTC7At0Am
                    };
                    if (!(await _dayJobServices.Create(driverDayJob)))
                    {
                        message = "Lỗi không xác định, xin mời thao tác lại";
                        TempData["UserMessage"] = SystemUtilites.SendSystemNotification(NotificationType.Error, message);
                        return View(model);
                    }
                }
                newTrans.DayJobId = driverDayJob.DayJobId;
                //create new TransInfo in SQL
                if (await _transInfoServices.CreateNewTransInfo(newTrans))
                {
                    message = "Chuyến vận chuyển đã được tạo";
                    TempData["UserMessage"] = SystemUtilites.SendSystemNotification(NotificationType.Success, message);
                    return RedirectToAction(actionName: "Manage");
                }
            }
            message = "Lỗi không xác định, xin mời thao tác lại";
            TempData["UserMessage"] = SystemUtilites.SendSystemNotification(NotificationType.Error, message);
            return View(model);
        }
        [HttpGet]
        public IActionResult Edit(string transId)
        {
            string message = String.Empty;
            var transInfo = _transInfoServices.GetTransport(transId);
            if (transInfo != null)
            {
                EditTransInfoViewModel model = new EditTransInfoViewModel()
                {
                    AdvanceMoney = transInfo.AdvanceMoney,
                    CargoTonnage = transInfo.CargoTonnage,
                    CargoTypes = transInfo.CargoTypes,
                    DriverId = transInfo.DayJob.DriverId,
                    IsCancel = transInfo.IsCancel,
                    IsCompleted = transInfo.IsCompleted,
                    Note = transInfo.Note,
                    ReasonCancel = transInfo.ReasonCancel,
                    ReturnOfAdvances = transInfo.ReturnOfAdvances,
                    RouteId = transInfo.RouteId,
                    TransportId = transInfo.TransportId,
                    VehicleId = transInfo.VehicleId,
                    Drivers = _userServices.GetAvailableUsers().ToList(),
                    Routes = _routeServices.GetAllRoutes().ToList(),
                    Vehicles = _vehicleServices.GetNotUseVehicles().ToList()
                };
                return View(model);
            }
            message = "Lỗi không xác định, xin mời thao tác lại";
            TempData["UserMessage"] = SystemUtilites.SendSystemNotification(NotificationType.Error, message);
            return RedirectToAction(actionName: "Manage");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditTransInfoViewModel model)
        {
            //get data for select elements if error
            model.Drivers = _userServices.GetAvailableUsers().ToList();
            model.Routes = _routeServices.GetAllRoutes().ToList();
            model.Vehicles = _vehicleServices.GetNotUseVehicles().ToList();
            string message = String.Empty;
            if (ModelState.IsValid)
            {
                //check cancel option and reason cancel
                if (model.IsCancel && String.IsNullOrEmpty(model.ReasonCancel))
                {
                    message = "Không thể để trống lý do hủy khi chọn hủy";
                    TempData["UserMessage"] = SystemUtilites.SendSystemNotification(NotificationType.Error, message);
                    return View(model);
                }
                if (!model.IsCancel && !String.IsNullOrEmpty(model.ReasonCancel))
                {
                    message = "Không thể điền lý do hủy nếu chưa chọn hủy";
                    TempData["UserMessage"] = SystemUtilites.SendSystemNotification(NotificationType.Error, message);
                    return View(model);
                }
                //if cancel and have reason cancel, write date complete transport
                if (model.IsCancel && !String.IsNullOrEmpty(model.ReasonCancel))
                {
                    DateTime localTimeUTC7 = SystemUtilites.ConvertToTimeZone(DateTime.UtcNow, "SE Asia Standard Time");
                    model.DateCompletedLocal = SystemUtilites.ConvertToTimeStamp(localTimeUTC7);
                    model.DateCompletedUTC = SystemUtilites.ConvertToTimeStamp(DateTime.UtcNow);
                }
                var transInfo = _transInfoServices.GetTransport(model.TransportId);
                if (transInfo != null)
                {
                    var user = await _userManager.GetUserAsync(User);
                    if (user != null)
                    {
                        if (await _transInfoServices.EditTransInfo(model, user.Id))
                        {
                            message = "Đơn vận chuyển đã được điều chỉnh thông tin";
                            TempData["UserMessage"] = SystemUtilites.SendSystemNotification(NotificationType.Success, message);
                            return RedirectToAction(actionName: "Manage");
                        }
                    }
                }
            }
            message = "Lỗi không xác định, xin mời thao tác lại";
            TempData["UserMessage"] = SystemUtilites.SendSystemNotification(NotificationType.Error, message);
            return View(model);
        }

    }
}
