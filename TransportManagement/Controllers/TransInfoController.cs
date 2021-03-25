using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using TransportManagement.Entities;
using TransportManagement.Models.Filter;
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
        [Authorize(Roles = "Quản trị viên hệ thống, Quản lý vận chuyển, Kế toán trưởng, Kế toán")]
        public async Task<IActionResult> Manage(FilterTransPortModel filter)
        {
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
            } else
            {
                TSstartDate = SystemUtilites.ConvertToTimeStamp(filter.StartDate);
            }
            if (SystemUtilites.ConvertToTimeStamp(filter.EndDate) == -62135596800) 
            {
                TSendDate = TSNowUTC7;
            } else TSendDate = SystemUtilites.ConvertToTimeStamp(filter.EndDate.AddDays(1));
            if (filter.Page == 0) filter.Page = 1;
            if (filter.PageSize == 0) filter.PageSize = model.PageSizeItem.Min();
            if (String.IsNullOrEmpty(filter.Search)) filter.Search = "";
            
            model.Items = await _transInfoServices.GetTransports(TSstartDate, TSendDate, filter.Search.ToLower());

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
            return View(model);
        }
        [HttpGet]
        [Authorize(Roles = "Quản trị viên hệ thống, Quản lý vận chuyển, Kế toán trưởng, Kế toán")]
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
            return RedirectToAction(actionName: "Manage");
        }
        [HttpGet]
        [Authorize(Roles = "Quản trị viên hệ thống, Quản lý vận chuyển")]
        public async Task<IActionResult> Create()
        {
            CreateTransInfoViewModel newTrans = new CreateTransInfoViewModel()
            {
                Drivers = _userServices.GetDriverUsers().ToList(),
                Routes = _routeServices.GetAllRoutes().ToList(),
                Vehicles = (await _vehicleServices.GetAllNotDeletedAndAvailableVehicles()).ToList()
            };
            return View(newTrans);
        }
        [HttpPost]
        [Authorize(Roles = "Quản trị viên hệ thống, Quản lý vận chuyển")]
        public async Task<IActionResult> Create(CreateTransInfoViewModel model)
        {
            //get local time at Timezone UTC 7
            DateTime localTimeUTC7 = SystemUtilites.ConvertToTimeZone(DateTime.UtcNow, "SE Asia Standard Time");
            //get timestamp of day at 0 AM
            double TStodayUTC7At0Am = SystemUtilites.ConvertToTimeStamp(localTimeUTC7.Date);
            //get timestamp now at utc
            double TSUTCNow = SystemUtilites.ConvertToTimeStamp(DateTime.UtcNow);
            //get data for select elements
            model.Drivers = _userServices.GetDriverUsers().ToList();
            model.Routes = _routeServices.GetAllRoutes().ToList();
            model.Vehicles = (await _vehicleServices.GetAllNotDeletedAndAvailableVehicles()).ToList();
            string message = String.Empty;
            if (ModelState.IsValid)
            {
                //check the vehicle is used
                string driverIdUseVehicle = await _vehicleServices.IsVehicleInUsedByAnotherDriver(model.DriverId, model.VehicleId);
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
                    var vehicle = await _vehicleServices.GetVehicle(newTrans.VehicleId);
                    if (vehicle != null)
                    {
                        if (!vehicle.IsInUse)
                        {
                            await _vehicleServices.MakeVehicleInUsed(vehicle);
                        }
                    }
                    var driver = await _userManager.FindByIdAsync(model.DriverId);
                    if (driver != null)
                    {
                        if (driver.IsAvailable)
                        {
                            await _userServices.MakeDriverIsBusy(driver);
                        }
                    }
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
                    return RedirectToAction(actionName: "Manage");
                }
                EditTransInfoViewModel model = new EditTransInfoViewModel()
                {
                    AdvanceMoney = transInfo.AdvanceMoney,
                    CargoTonnage = transInfo.CargoTonnage,
                    CargoTypes = transInfo.CargoTypes,
                    DriverId = transInfo.DayJob.DriverId,
                    IsCancel = transInfo.IsCancel,
                    Note = transInfo.Note,
                    ReasonCancel = transInfo.ReasonCancel,
                    ReturnOfAdvances = transInfo.ReturnOfAdvances,
                    RouteId = transInfo.RouteId,
                    TransportId = transInfo.TransportId,
                    VehicleId = transInfo.VehicleId,
                    Drivers = _userServices.GetAvailableUsers().ToList(),
                    Routes = _routeServices.GetAllRoutes().ToList(),
                    Vehicles = (await _vehicleServices.GetAllNotDeletedAndAvailableVehicles()).ToList()
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
            model.Vehicles = (await _vehicleServices.GetAllNotDeletedAndAvailableVehicles()).ToList();
            string message = String.Empty;
            if (ModelState.IsValid)
            {
                //check the vehicle is used
                string driverIdUseVehicle = await _vehicleServices.IsVehicleInUsedByAnotherDriver(model.DriverId, model.VehicleId);
                if (!String.IsNullOrEmpty(driverIdUseVehicle))
                {
                    var driverUseVehicle = _userServices.GetUser(driverIdUseVehicle);
                    message = $"Xe đang được sử dụng bởi {driverUseVehicle.FullName}";
                    TempData["UserMessage"] = SystemUtilites.SendSystemNotification(NotificationType.Error, message);
                    return View(model);
                }
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

        [HttpGet]
        [Authorize(Roles = "Quản trị viên hệ thống")]
        public async Task<IActionResult> ViewHistory(string transportId, int page, int pageSize)
        {
            ViewBag.transId = transportId;
            PaginationViewModel<EditTransportInformation> model = new PaginationViewModel<EditTransportInformation>();
            if (page == 0) page = 1;
            if (pageSize == 0) pageSize = model.PageSizeItem.Min();
            List<EditTransportInformation> histories = (await _transInfoServices.Histories(transportId)).ToList();
            if (histories == null)
            {
                histories = new List<EditTransportInformation>();
            }
            model.Pager = new Pager(histories.Count, page, pageSize);
            model.Items = histories.Skip((page - 1) * pageSize).Take(pageSize);
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Quản trị viên hệ thống, Quản lý vận chuyển")]
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
                    return RedirectToAction(actionName: "Manage");
                }
                if (await _transInfoServices.DoneTransInfo(trans, user.Id))
                {
                    var vehicle = await _vehicleServices.GetVehicle(trans.VehicleId);
                    var listTransportByVehicle = await _transInfoServices.GetTransportsNotFinishByVehicle(vehicle.VehicleId);
                    if (listTransportByVehicle != null && vehicle != null)
                    {
                        if (!listTransportByVehicle.Any() && vehicle.IsInUse)
                        {
                            await _vehicleServices.MakeVehicleIsFree(vehicle);
                        }
                    }
                    var driver = await _userManager.FindByIdAsync(trans.DayJob.DriverId);
                    var listTransportByDriver = await _transInfoServices.GetTransportsNotFinishByDriver(driver.Id);
                    if (listTransportByDriver != null && driver != null)
                    {
                        if (!listTransportByDriver.Any() && !driver.IsAvailable)
                        {
                            await _userServices.MakeDriverIsFree(driver);
                        }
                    }
                    message = "Đã hoàn thành chuyến vận chuyển";
                    TempData["UserMessage"] = SystemUtilites.SendSystemNotification(NotificationType.Success, message);
                    return RedirectToAction(actionName: "Manage");
                }
            }
            message = "Lỗi không xác định, xin mời thao tác lại";
            TempData["UserMessage"] = SystemUtilites.SendSystemNotification(NotificationType.Error, message);
            return RedirectToAction(actionName: "Manage");
        }
    }
}
