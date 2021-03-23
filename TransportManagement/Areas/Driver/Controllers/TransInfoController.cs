using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransportManagement.Entities;
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
        [HttpGet]
        public IActionResult Details(string transportId)
        {
            string message = String.Empty;
            var transInfo = _transInfoServices.GetTransport(transportId);
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
                    Drivers = _userServices.GetAvailableUsers().ToList(),
                    Routes = _routeServices.GetAllRoutes().ToList(),
                    Vehicles = _vehicleServices.GetNotUseVehicles().ToList()
                };
                return View(model);
            }
            message = "Lỗi không xác định, xin mời thao tác lại";
            TempData["UserMessage"] = SystemUtilites.SendSystemNotification(NotificationType.Error, message);
            return RedirectToAction(actionName: "Index");
        }
        
        [HttpGet]
        public IActionResult Edit(string transId)
        {
            string message = String.Empty;
            var transInfo = _transInfoServices.GetTransport(transId);
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
            model.Routes = RouteServices.GetAllRoutes().ToList();
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
                        if (model.CargoTonnage > 0)
                        {
                            var vehicle = await _vehicleServices.GetVehicle(model.VehicleId);
                            var route = _routeServices.GetRoute(model.RouteId);
                            model.ReturnOfAdvances = model.CargoTonnage * vehicle.FuelConsumptionPerTone * route.Distance;
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
        public IActionResult ViewHistory(string transportId)
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> DoneTransInfo(string transportId)
        {
            string message = String.Empty;
            var trans = _transInfoServices.GetTransport(transportId);
            var user = await _userManager.GetUserAsync(User);
            if (trans != null)
            {
                if (trans.DateCompletedLocal > 0)
                {
                    message = "Chuyến vận chuyển đã được kết thúc";
                    TempData["UserMessage"] = SystemUtilites.SendSystemNotification(NotificationType.Error, message);
                    return RedirectToAction(actionName: "Index");
                }
                if (await _transInfoServices.DoneTransInfo(trans, user.Id))
                {
                    message = "Đã hoàn thành chuyến vận chuyển";
                    TempData["UserMessage"] = SystemUtilites.SendSystemNotification(NotificationType.Success, message);
                    return RedirectToAction(actionName: "Index");
                }
            }
            message = "Lỗi không xác định, xin mời thao tác lại";
            TempData["UserMessage"] = SystemUtilites.SendSystemNotification(NotificationType.Error, message);
            return RedirectToAction(actionName: "Index");
        }
    }
}
