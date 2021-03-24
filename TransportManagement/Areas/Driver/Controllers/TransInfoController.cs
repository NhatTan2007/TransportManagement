using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransportManagement.Entities;
using TransportManagement.Models.Filter;
using TransportManagement.Models.Pagination;
using TransportManagement.Models.TransportInformation;
using TransportManagement.Services.IServices;
using TransportManagement.Utilities;

namespace TransportManagement.Areas.Driver.Controllers
{
    [Area("Driver")]
    [Authorize(Roles = "Lái xe")]
    public class TransInfoController : Controller
    {
        private readonly IUserServices _userServices;
        private readonly IRouteServices _routeServices;
        private readonly IVehicleServices _vehicleServices;
        private readonly ITransInfoServices _transInfoServices;
        private readonly IDayJobServices _dayJobServices;
        private readonly UserManager<AppIdentityUser> _userManager;

        public IRouteServices RouteServices => _routeServices;

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

        public async Task<IActionResult> TransFinish(FilterTransPortModel filter)
        {
            var user = await _userManager.GetUserAsync(User);
            //get local time at Timezone UTC 7
            DateTime localTimeUTC7 = SystemUtilites.ConvertToTimeZone(DateTime.UtcNow, "SE Asia Standard Time");
            //get timestamp of day at 0 AM
            double TSNowUTC7 = SystemUtilites.ConvertToTimeStamp(localTimeUTC7);
            //get timestamp 0 AM 1st day of month
            DateTime startMonth = new DateTime(localTimeUTC7.Year, localTimeUTC7.Month, 01);
            double TSMonthUTC7 = SystemUtilites.ConvertToTimeStamp(startMonth);
            double TSstartDate = 0;
            double TSendDate = 0;
            PaginationViewModel<TransInfoViewModel> model = new PaginationViewModel<TransInfoViewModel>();
            if (SystemUtilites.ConvertToTimeStamp(filter.StartDate) == -62135596800)
            {
                TSstartDate = TSMonthUTC7;
            }
            else
            {
                TSstartDate = SystemUtilites.ConvertToTimeStamp(filter.StartDate);
            }
            if (SystemUtilites.ConvertToTimeStamp(filter.EndDate) == -62135596800)
            {
                TSendDate = TSNowUTC7;
            }
            else TSendDate = SystemUtilites.ConvertToTimeStamp(filter.EndDate.AddDays(1));
            if (filter.Page == 0) filter.Page = 1;
            if (filter.PageSize == 0) filter.PageSize = model.PageSizeItem.Min();
            if (String.IsNullOrEmpty(filter.Search)) filter.Search = "";

            model.Items = await _transInfoServices.GetTransportsDoneByDriver(TSstartDate, TSendDate, user.Id);

            int countItems = 0;
            if (model.Items != null)
            {
                if (model.Items.Any())
                {
                    countItems = model.Items.Count();
                }
            }
            model.Search = filter.Search;
            model.StartDate = filter.StartDate;
            model.EndDate = filter.EndDate;
            model.Items = model.Items.Skip((filter.Page - 1) * filter.PageSize).Take(filter.PageSize);
            model.Pager = new Pager(countItems, filter.Page, filter.PageSize);
            ViewBag.Search = filter.Search;
            return View(model);
        }
        public async Task<IActionResult> TransNotFinishYet(FilterTransPortModel filter)
        {
            var user = await _userManager.GetUserAsync(User);
            //get local time at Timezone UTC 7
            DateTime localTimeUTC7 = SystemUtilites.ConvertToTimeZone(DateTime.UtcNow, "SE Asia Standard Time");
            //get timestamp of day at 0 AM
            double TSNowUTC7 = SystemUtilites.ConvertToTimeStamp(localTimeUTC7);
            //get timestamp 0 AM 1st day of month
            DateTime startMonth = new DateTime(localTimeUTC7.Year, localTimeUTC7.Month, 01);
            double TSMonthUTC7 = SystemUtilites.ConvertToTimeStamp(startMonth);
            double TSstartDate = 0;
            double TSendDate = 0;
            PaginationViewModel<TransInfoViewModel> model = new PaginationViewModel<TransInfoViewModel>();
            if (SystemUtilites.ConvertToTimeStamp(filter.StartDate) == -62135596800)
            {
                TSstartDate = TSMonthUTC7;
            }
            else
            {
                TSstartDate = SystemUtilites.ConvertToTimeStamp(filter.StartDate);
            }
            if (SystemUtilites.ConvertToTimeStamp(filter.EndDate) == -62135596800)
            {
                TSendDate = TSNowUTC7;
            }
            else TSendDate = SystemUtilites.ConvertToTimeStamp(filter.EndDate.AddDays(1));
            if (filter.Page == 0) filter.Page = 1;
            if (filter.PageSize == 0) filter.PageSize = model.PageSizeItem.Min();
            if (String.IsNullOrEmpty(filter.Search)) filter.Search = "";

            model.Items = await _transInfoServices.GetTransportsNotDoneByDriver(TSstartDate, TSendDate, user.Id);

            int countItems = 0;
            if (model.Items != null)
            {
                if (model.Items.Any())
                {
                    countItems = model.Items.Count();
                }
            }
            model.Search = filter.Search;
            model.StartDate = filter.StartDate;
            model.EndDate = filter.EndDate;
            model.Items = model.Items.Skip((filter.Page - 1) * filter.PageSize).Take(filter.PageSize);
            model.Pager = new Pager(countItems, filter.Page, filter.PageSize);
            ViewBag.Search = filter.Search;
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Details(string transportId)
        {
            string message = String.Empty;
            var transInfo = await _transInfoServices.GetTransport(transportId);
            if (transInfo != null)
            {
                DetailTransInfoViewModel model = new DetailTransInfoViewModel()
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
                    DateCompletedLocal = transInfo.DateCompletedLocal,
                    DateStartLocal = transInfo.DateStartLocal,
                    Drivers = _userServices.GetDriverUsers().ToList(),
                    Routes = _routeServices.GetAllRoutes().ToList(),
                    Vehicles = (await _vehicleServices.GetAllVehicles()).ToList()
                };
                return View(model);
            }
            message = "Lỗi không xác định, xin mời thao tác lại";
            TempData["UserMessage"] = SystemUtilites.SendSystemNotification(NotificationType.Error, message);
            return RedirectToAction(actionName: "Index");
        }
        
        [HttpGet]
        public async Task<IActionResult> Edit(string transId)
        {
            string message = String.Empty;
            var transInfo = await _transInfoServices.GetTransport(transId);
            if (transInfo != null)
            {
                if (transInfo.DateCompletedLocal > 0)
                {
                    message = "Không thể chỉnh sửa nếu đã HOÀN THÀNH hoặc HỦY";
                    TempData["UserMessage"] = SystemUtilites.SendSystemNotification(NotificationType.Error, message);
                    return RedirectToAction(actionName: "Index", controllerName: "Home");
                }
                EditTransInfoViewModel model = new EditTransInfoViewModel()
                {
                    AdvanceMoney = transInfo.AdvanceMoney,
                    CargoTonnage = transInfo.CargoTonnage,
                    CargoTypes = transInfo.CargoTypes,
                    DriverId = transInfo.DayJob.DriverId,
                    Note = transInfo.Note,
                    ReturnOfAdvances = transInfo.ReturnOfAdvances,
                    RouteId = transInfo.RouteId,
                    TransportId = transInfo.TransportId,
                    VehicleId = transInfo.VehicleId,
                    Drivers = _userServices.GetAvailableUsers().ToList(),
                    Routes = RouteServices.GetAllRoutes().ToList(),
                    Vehicles = (await _vehicleServices.GetNotUseVehicles()).ToList()
                };
                return View(model);
            }
            message = "Lỗi không xác định, xin mời thao tác lại";
            TempData["UserMessage"] = SystemUtilites.SendSystemNotification(NotificationType.Error, message);
            return RedirectToAction(actionName: "Index", controllerName: "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditTransInfoViewModel model)
        {
            //get data for select elements if error
            model.Drivers = _userServices.GetAvailableUsers().ToList();
            model.Routes = RouteServices.GetAllRoutes().ToList();
            model.Vehicles = (await _vehicleServices.GetNotUseVehicles()).ToList();
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
                        if (model.CargoTonnage > 0)
                        {
                            var vehicle = await _vehicleServices.GetVehicle(model.VehicleId);
                            var route = _routeServices.GetRoute(model.RouteId);
                            model.ReturnOfAdvances = model.CargoTonnage * vehicle.FuelConsumptionPerTone * vehicle.Fuel.FuelPrice * route.Distance;
                        }
                        if (await _transInfoServices.EditTransInfo(model, user.Id))
                        {
                            message = "Đơn vận chuyển đã được điều chỉnh thông tin";
                            TempData["UserMessage"] = SystemUtilites.SendSystemNotification(NotificationType.Success, message);
                            return RedirectToAction(actionName: "Index");
                        }
                    }
                }
            }
            message = "Lỗi không xác định, xin mời thao tác lại";
            TempData["UserMessage"] = SystemUtilites.SendSystemNotification(NotificationType.Error, message);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> DoneTransInfo(string transportId)
        {
            string message = String.Empty;
            var trans = await _transInfoServices.GetTransport(transportId);
            var user = await _userManager.GetUserAsync(User);
            if (trans != null)
            {
                if (trans.DateCompletedLocal > 0)
                {
                    message = "Chuyến vận chuyển đã được kết thúc";
                    TempData["UserMessage"] = SystemUtilites.SendSystemNotification(NotificationType.Error, message);
                    return RedirectToAction(actionName: "Index", controllerName: "Home");
                }
                if (await _transInfoServices.DoneTransInfo(trans, user.Id))
                {
                    message = "Đã hoàn thành chuyến vận chuyển";
                    TempData["UserMessage"] = SystemUtilites.SendSystemNotification(NotificationType.Success, message);
                    return RedirectToAction(actionName: "Index", controllerName: "Home");
                }
            }
            message = "Lỗi không xác định, xin mời thao tác lại";
            TempData["UserMessage"] = SystemUtilites.SendSystemNotification(NotificationType.Error, message);
            return RedirectToAction(actionName: "Index", controllerName: "Home");
        }
    }
}
