using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransportManagement.DbContexts;
using TransportManagement.Entities;
using TransportManagement.Models;
using TransportManagement.Models.Location;
using TransportManagement.Models.Pagination;
using TransportManagement.Services;

namespace TransportManagement.Controllers
{
    public class LocationController : Controller
    {
        private readonly ILocationServices _locationServices;

        public LocationController(ILocationServices locationServices)
        {
            _locationServices = locationServices;
        }
        public IActionResult Index(int page, int pageSize, string search)
        {
            int countTotalUsers = _locationServices.CountLocations();
            PaginationViewModel<LocationViewModel> model = new PaginationViewModel<LocationViewModel>();
            if (page == 0) page = 1;
            if (pageSize == 0) pageSize = model.PageSizeItem.Min();
            model.Pager = new Pager(countTotalUsers, page, pageSize);
            if (String.IsNullOrEmpty(search))
            {
                model.Items = _locationServices.GetAllLocations(page, pageSize).ToList();
            }
            else
            {
                model.Items = _locationServices.GetAllLocations(page, pageSize, search).ToList();
            }
            ViewBag.Search = search;
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateLocationViewModel model)
        {
            if (ModelState.IsValid)
            {
                Location newLocation = new Location()
                {
                    LocationId = Guid.NewGuid().ToString(),
                    LocationName = model.LocationName
                };
                if (await _locationServices.CreateLocation(newLocation)) 
                {
                    var userMessage = new MessageVM() { CssClassName = "alert alert-success", Title = "Thành công", Message = "Tạo địa điểm mới thành công" };
                    TempData["UserMessage"] = JsonConvert.SerializeObject(userMessage);
                }
                else
                {
                    var userMessage = new MessageVM() { CssClassName = "alert alert-danger", Title = "Không thành công", Message = "Đã có lỗi khi thao tác, xin mời thử lại" };
                    TempData["UserMessage"] = JsonConvert.SerializeObject(userMessage);
                };
            }
            return RedirectToAction(actionName: "Index", controllerName: "Location");
        }

        public async Task<IActionResult> Delete(string locationId)
        {
            var locationDel = _locationServices.GetLocation(locationId);
            if (await _locationServices.DeleteLocation(locationDel))
            {
                var userMessage = new MessageVM() { CssClassName = "alert alert-success ", Title = "Thành công", Message = "Đã xóa địa điểm thành công" };
                TempData["UserMessage"] = JsonConvert.SerializeObject(userMessage);
            }
            else
            {
                var userMessage = new MessageVM() { CssClassName = "alert alert-danger", Title = "Không thành công", Message = "Đã có lỗi khi thao tác, xin mời thử lại" };
                TempData["UserMessage"] = JsonConvert.SerializeObject(userMessage);
            }
            return RedirectToAction(actionName: "Index", controllerName: "Location");
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditLocationViewModel model)
        {
            MessageVM userMessage = new MessageVM();
            if (ModelState.IsValid)
            {
                if (await _locationServices.EditLocation(model))
                {
                    userMessage = new MessageVM() { CssClassName = "alert alert-success ", Title = "Thành công", Message = "Chỉnh sửa địa điểm thành công" };
                    TempData["UserMessage"] = JsonConvert.SerializeObject(userMessage);
                    return RedirectToAction(actionName: "Index");
                }
            }
            userMessage = new MessageVM() { CssClassName = "alert alert-danger ", Title = "Không thành công", Message = "Thao tác thất bại, xin mời thử lại" };
            TempData["UserMessage"] = JsonConvert.SerializeObject(userMessage);
            return RedirectToAction(actionName: "Index");
        }
    }
}
