using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransportManagement.Entities;
using TransportManagement.Models;
using TransportManagement.Models.Pagination;
using TransportManagement.Models.VehicleBrand;
using TransportManagement.Services.IServices;

namespace TransportManagement.Controllers
{
    public class VehicleBrandController : Controller
    {
        private readonly IVehicleBrandServices _brandServices;

        public VehicleBrandController(IVehicleBrandServices brandServices)
        {
            _brandServices = brandServices;
        }
        public IActionResult Index(int page, int pageSize, string search)
        {
            int countTotalLocations = _brandServices.CountBrands();
            PaginationViewModel<VehicleBrandViewModel> model = new PaginationViewModel<VehicleBrandViewModel>();
            if (page == 0) page = 1;
            if (pageSize == 0) pageSize = model.PageSizeItem.Min();
            model.Pager = new Pager(countTotalLocations, page, pageSize);
            if (String.IsNullOrEmpty(search))
            {
                model.Items = _brandServices.GetAllBrands(page, pageSize).ToList();
            }
            else
            {
                model.Items = _brandServices.GetAllBrands(page, pageSize, search).ToList();
            }
            ViewBag.Search = search;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateVehicleBrandViewModel model)
        {
            if (ModelState.IsValid)
            {
                VehicleBrand newBrand = new VehicleBrand()
                {
                    BrandId = Guid.NewGuid().ToString(),
                    BrandName = model.BrandName
                };
                if (await _brandServices.CreateBrand(newBrand))
                {
                    var userMessage = new MessageVM() { CssClassName = "alert alert-success", Title = "Thành công", Message = "Tạo nhãn hiệu mới thành công" };
                    TempData["UserMessage"] = JsonConvert.SerializeObject(userMessage);
                }
                else
                {
                    var userMessage = new MessageVM() { CssClassName = "alert alert-danger", Title = "Không thành công", Message = "Đã có lỗi khi thao tác, xin mời thử lại" };
                    TempData["UserMessage"] = JsonConvert.SerializeObject(userMessage);
                };
            }
            return RedirectToAction(actionName: "Index");
        }

        public async Task<IActionResult> Delete(string brandId)
        {
            var brandDel = _brandServices.GetBrand(brandId);
            if (await _brandServices.DeleteBrand(brandDel))
            {
                var userMessage = new MessageVM() { CssClassName = "alert alert-success ", Title = "Thành công", Message = "Đã xóa nhãn hiệu thành công" };
                TempData["UserMessage"] = JsonConvert.SerializeObject(userMessage);
            }
            else
            {
                var userMessage = new MessageVM() { CssClassName = "alert alert-danger", Title = "Không thành công", Message = "Đã có lỗi khi thao tác, xin mời thử lại" };
                TempData["UserMessage"] = JsonConvert.SerializeObject(userMessage);
            }
            return RedirectToAction(actionName: "Index");
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditVehicleBrandViewModel model)
        {
            MessageVM userMessage = new MessageVM();
            if (ModelState.IsValid)
            {
                if (await _brandServices.EditBrand(model))
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
