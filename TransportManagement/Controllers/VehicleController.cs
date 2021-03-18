using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransportManagement.Entities;
using TransportManagement.Models.Pagination;
using TransportManagement.Models.Vehicle;
using TransportManagement.Models.VehicleBrand;
using TransportManagement.Services.IServices;
using TransportManagement.Utilities;

namespace TransportManagement.Controllers
{
    public class VehicleController : Controller
    {
        private readonly IVehicleServices _vehicleServices;
        private readonly IVehicleBrandServices _brandServices;

        public VehicleController(IVehicleServices vehicleServices,
                                    IVehicleBrandServices brandServices)
        {
            _vehicleServices = vehicleServices;
            _brandServices = brandServices;
        }
        public IActionResult Index(int page, int pageSize, string search)
        {
            int countTotalVehicles = _vehicleServices.CountVehicles();
            PaginationViewModel<VehicleViewModel> model = new PaginationViewModel<VehicleViewModel>();
            if (page == 0) page = 1;
            if (pageSize == 0) pageSize = model.PageSizeItem.Min();
            model.Pager = new Pager(countTotalVehicles, page, pageSize);
            if (String.IsNullOrEmpty(search))
            {
                model.Items = _vehicleServices.GetAllVehicles(page, pageSize).ToList();
            }
            else
            {
                model.Items = _vehicleServices.GetAllVehicles(page, pageSize, search).ToList();
            }
            if (_brandServices.CountBrands() > 0)
            {
                ViewBag.Brands = _brandServices.GetAllBrands().ToList();
            }
            ViewBag.Search = search;
            return View(model);
        }
        [HttpGet]
        public IActionResult Create()
        {
            var model = new CreateVehicleViewModel()
            {
                VehicleBrands = _brandServices.GetAllBrands().ToList()
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateVehicleViewModel model)
        {
            string message = String.Empty;
            if (ModelState.IsValid)
            {
                var newVehicle = new Vehicle()
                {
                    LicensePlate = model.LicensePlate,
                    VehicleName = model.VehicleName,
                    FuelConsumptionPerTone = model.FuelConsumptionPerTone,
                    IsAvailable = model.IsAvailable,
                    IsInUse = model.IsInUse,
                    VehicleBrandId = model.VehicleBrandId,
                    Specifications = model.Specifications,
                    VehiclePayload = model.VehiclePayload
                };
                if (await _vehicleServices.CreateVehicle(newVehicle))
                {
                    message = "Phương tiện mới đã được tạo thành công";
                    TempData["UserMessage"] = SystemUtilites.SendSystemNotification(NotificationType.Success, message);
                    return RedirectToAction(actionName: "Index");
                }
            }
            message = "Lỗi không xác định, xin mời thao tác lại";
            TempData["UserMessage"] = SystemUtilites.SendSystemNotification(NotificationType.Error, message);
            return View(model);
        }
        [HttpGet]
        public IActionResult Edit(int vehicleId)
        {
            string message = String.Empty;
            var vehicle = _vehicleServices.GetVehicle(vehicleId);
            if (vehicle != null)
            {
                EditVehicleViewModel vehicleEdit = new EditVehicleViewModel()
                {
                    VehicleId = vehicle.VehicleId,
                    LicensePlate = vehicle.LicensePlate,
                    VehicleName = vehicle.VehicleName,
                    FuelConsumptionPerTone = vehicle.FuelConsumptionPerTone,
                    IsAvailable = vehicle.IsAvailable,
                    IsInUse = vehicle.IsInUse,
                    VehicleBrandId = vehicle.VehicleBrandId,
                    Specifications = vehicle.Specifications,
                    VehiclePayload = vehicle.VehiclePayload,
                    VehicleBrands = _brandServices.GetAllBrands().ToList()
                };
                return View(vehicleEdit);
            }
            message = "Lỗi không xác định, xin mời thao tác lại";
            TempData["UserMessage"] = SystemUtilites.SendSystemNotification(NotificationType.Error, message);
            return RedirectToAction(actionName: "Index");
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditVehicleViewModel model)
        {
            string message = String.Empty;
            model.VehicleBrands = _brandServices.GetAllBrands().ToList();
            if (ModelState.IsValid)
            {
                if (await _vehicleServices.EditVehicle(model))
                {
                    message = "Điều chỉnh phương tiện thành công";
                    TempData["UserMessage"] = SystemUtilites.SendSystemNotification(NotificationType.Success, message);
                    return RedirectToAction(actionName: "Index");
                }
            }
            message = "Lỗi không xác định, xin mời thao tác lại";
            TempData["UserMessage"] = SystemUtilites.SendSystemNotification(NotificationType.Error, message);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int vehicleId)
        {
            string message = String.Empty;
            var vehicleDel = _vehicleServices.GetVehicle(vehicleId);
            if (vehicleDel != null)
            {
                if (await _vehicleServices.DeleteVehicle(vehicleDel))
                {
                    message = "Xóa phương tiện thành công";
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
