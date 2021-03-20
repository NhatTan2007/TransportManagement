using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransportManagement.Entities;
using TransportManagement.Models.Pagination;
using TransportManagement.Models.Role;
using TransportManagement.Utilities;

namespace TransportManagement.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<AppIdentityRole> _roleManager;

        public RoleController(RoleManager<AppIdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        [HttpGet]
        public IActionResult Index(int page, int pageSize)
        {
            int countRoles = _roleManager.Roles.Count();
            PaginationViewModel<RoleViewModel> model = new PaginationViewModel<RoleViewModel>();
            if (page == 0) page = 1;
            if (pageSize == 0) pageSize = model.PageSizeItem.Min();
            model.Pager = new Pager(countRoles, page, pageSize);
            model.Items = GetRoles(page, pageSize);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRoleViewModel model)
        {
            string message = String.Empty;
            if (ModelState.IsValid)
            {
                AppIdentityRole newRole = new AppIdentityRole()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = model.RoleName,
                    RolePriority = model.RolePriority
                };
                var result = await _roleManager.CreateAsync(newRole);
                if (result.Succeeded)
                {
                    message = "Phân quyền mới đã được tạo";
                    TempData["UserMessage"] = SystemUtilites.SendSystemNotification(NotificationType.Success, message);
                    return RedirectToAction(actionName: "Index");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            message = "Lỗi không xác định, xin mời thao tác lại";
            TempData["UserMessage"] = SystemUtilites.SendSystemNotification(NotificationType.Error, message);
            return RedirectToAction(actionName: "Index");
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditRoleViewModel model)
        {
            string message = String.Empty;
            if (ModelState.IsValid)
            {
                var roleEdit = await _roleManager.FindByIdAsync(model.RoleId);
                if (roleEdit != null)
                {
                    roleEdit.Name = model.RoleName;
                    roleEdit.RolePriority = model.RolePriority;
                    var result = await _roleManager.UpdateAsync(roleEdit);
                    if (result.Succeeded)
                    {
                        message = "Phân quyền đã được điều chỉnh";
                        TempData["UserMessage"] = SystemUtilites.SendSystemNotification(NotificationType.Success, message);
                        return RedirectToAction(actionName: "Index");
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            message = "Lỗi không xác định, xin mời thao tác lại";
            TempData["UserMessage"] = SystemUtilites.SendSystemNotification(NotificationType.Error, message);
            return RedirectToAction(actionName: "Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string roleId)
        {
            string message = String.Empty;
            var roleDel = await _roleManager.FindByIdAsync(roleId);
            if (roleDel != null)
            {
                var result = await _roleManager.DeleteAsync(roleDel);
                if (result.Succeeded)
                {
                    message = "Phân quyền đã được xóa";
                    TempData["UserMessage"] = SystemUtilites.SendSystemNotification(NotificationType.Success, message);
                    return RedirectToAction(actionName: "Index");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            message = "Lỗi không xác định, xin mời thao tác lại";
            TempData["UserMessage"] = SystemUtilites.SendSystemNotification(NotificationType.Error, message);
            return RedirectToAction(actionName: "Index");
        }

        [HttpGet]
        public async Task<IActionResult> GetRole(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            RoleViewModel viewRole = new RoleViewModel()
            {
                RoleId = role.Id,
                RoleName = role.Name,
                RolePriority = role.RolePriority
            };
            return Ok(viewRole);
        }

        public IEnumerable<RoleViewModel> GetRoles(int page, int pageSize)
        {
            return _roleManager.Roles.OrderBy(r => r.RolePriority)
                                        .Skip((page - 1) * pageSize)
                                        .Take(pageSize)
                                        .Select(r => new RoleViewModel()
                                        {
                                            RoleId = r.Id,
                                            RoleName = r.Name,
                                            RolePriority = r.RolePriority
                                        });
        }
    }
}
