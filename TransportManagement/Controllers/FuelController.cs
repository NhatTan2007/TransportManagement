using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransportManagement.Entities;
using TransportManagement.Models.Fuel;
using TransportManagement.Services.IServices;
using TransportManagement.Utilities;

namespace TransportManagement.Controllers
{
    [Authorize]
    public class FuelController : Controller
    {
        private readonly IFuelServices _fuelServices;

        public FuelController(IFuelServices fuelServices)
        {
            _fuelServices = fuelServices;
        }
        public IActionResult Index()
        {
            List<FuelViewModel> fuels = _fuelServices.GetFuels().ToList();
            return View(fuels);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateFuelViewModel model)
        {
            string message = String.Empty;
            if (ModelState.IsValid)
            {
                if (await _fuelServices.Create(model))
                {
                    message = "Nhiên liệu mới đã được tạo";
                    TempData["UserMessage"] = SystemUtilites.SendSystemNotification(NotificationType.Success, message);
                    return RedirectToAction(actionName: "Index");
                }
            }
            message = "Lỗi không xác định, xin mời thao tác lại";
            TempData["UserMessage"] = SystemUtilites.SendSystemNotification(NotificationType.Error, message);
            return RedirectToAction(actionName: "Index");
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditFuelViewModel model)
        {
            string message = String.Empty;
            if (ModelState.IsValid)
            {
                var fuel = _fuelServices.GetFuel(model.FuelId);
                if (fuel != null)
                {
                    if (await _fuelServices.Edit(model))
                    {
                        message = "Thông tin nhiên liệu đã được chỉnh sửa";
                        TempData["UserMessage"] = SystemUtilites.SendSystemNotification(NotificationType.Success, message);
                        return RedirectToAction(actionName: "Index");
                    }
                }
            }
            message = "Lỗi không xác định, xin mời thao tác lại";
            TempData["UserMessage"] = SystemUtilites.SendSystemNotification(NotificationType.Error, message);
            return RedirectToAction(actionName: "Index");
        }

        [HttpGet]
        public IActionResult GetFuel(int fuelId)
        {
            var fuel = _fuelServices.GetFuel(fuelId);
            if (fuel != null)
            {
                return Ok(fuel);
            }
            return NotFound();
        }
    }
}
