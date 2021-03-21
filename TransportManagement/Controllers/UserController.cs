using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TransportManagement.DbContexts;
using TransportManagement.Entities;
using TransportManagement.Models.Pagination;
using TransportManagement.Models.User;
using TransportManagement.Services.IServices;
using TransportManagement.Utilities;

namespace TransportManagement.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<AppIdentityUser> _userManager;
        private readonly SignInManager<AppIdentityUser> _signInManager;
        private readonly RoleManager<AppIdentityRole> _roleManager;
        private readonly IUserServices _userServices;
        private readonly IWebHostEnvironment _webEnv;
        private readonly IRoleServices _roleServices;

        public UserController(UserManager<AppIdentityUser> userManager,
                                SignInManager<AppIdentityUser> signInManager,
                                    RoleManager<AppIdentityRole> roleManager,
                                        IUserServices userServices,
                                            IWebHostEnvironment webEnv,
                                                IRoleServices roleServices)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userServices = userServices;
            _webEnv = webEnv;
            _roleServices = roleServices;
            _roleManager = roleManager;
        }
        [HttpGet]
        public async Task<IActionResult> Index(int page, int pageSize, string search)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                int userRolePriority = await _roleServices.GetUserRolePriority(user);
                int countTotalUsers = _userServices.CountUsers();
                PaginationViewModel<UserViewModel> model = new PaginationViewModel<UserViewModel>();
                if (page == 0) page = 1;
                if (pageSize == 0) pageSize = model.PageSizeItem.Min();
                model.Pager = new Pager(countTotalUsers, page, pageSize);
                if (String.IsNullOrEmpty(search))
                {
                    if (User.IsInRole("Quản trị viên hệ thống"))
                    {
                        model.Items = _userServices.GetAllUsers(page, pageSize, userRolePriority).ToList();
                    }
                    else
                    {
                        model.Items = _userServices.GetAllActiveUsers(page, pageSize, userRolePriority).ToList();
                    }
                }
                else
                {
                    if (User.IsInRole("Quản trị viên hệ thống"))
                    {
                        model.Items = _userServices.GetAllUsers(page, pageSize, userRolePriority, search).ToList();
                    }
                    else
                    {
                        model.Items = _userServices.GetAllActiveUsers(page, pageSize, userRolePriority, search).ToList();
                    }
                }
                ViewBag.Search = search;
                return View(model);
            }
            return RedirectToAction(actionName: "Login", controllerName: "Account");
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var user = await _userManager.GetUserAsync(User);
            string message = String.Empty;
            if (user != null)
            {
                CreateUserViewModel model = new CreateUserViewModel()
                {
                    Roles = await _roleServices.GetRoles(user.Id)
                };
                return View(model);
            }
            message = "Lỗi không xác định, xin mời thao tác lại";
            TempData["UserMessage"] = SystemUtilites.SendSystemNotification(NotificationType.Error, message);
            return RedirectToAction(actionName: "Index");
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            model.Roles = await _roleServices.GetRoles(user.Id);
            string message = String.Empty;
            if (ModelState.IsValid)
            {
                string folderPath = Path.Combine(_webEnv.WebRootPath, "images", "account");
                string fileName = String.Empty;
                //Upload avatar if user using avatar
                if (model.Avatar != null)
                {
                    fileName = $"{Guid.NewGuid()}_{model.Avatar.FileName}";
                }
                else
                {
                    fileName = "noavatar.png";
                }
                string filePath = Path.Combine(folderPath, fileName);
                //Get RolePriority of new user
                var userRole = await _roleManager.FindByIdAsync(model.RoleId);
                //Create new user
                AppIdentityUser newUser = new AppIdentityUser()
                {
                    Id = Guid.NewGuid().ToString(),
                    Email = model.Email,
                    FirstName = model.FirstName,
                    MiddleName = model.MiddleName,
                    LastName = model.LastName,
                    UserName = model.UserName,
                    IsActive = model.IsActive,
                    IsAvailable = model.IsAvailable,
                    PhoneNumber = model.PhoneNumber,
                    Avatar = fileName,
                    RolePriority = userRole.RolePriority,
                    JobTitle = userRole.Name
                };
                try
                {
                    var result = await _userManager.CreateAsync(newUser, model.Password);
                    if (result.Succeeded)
                    {
                        //upload avatar if new user has avatar
                        if (model.Avatar != null)
                        {
                            using (FileStream fs = new FileStream(filePath, FileMode.Create))
                            {
                                try
                                {
                                    await model.Avatar.CopyToAsync(fs);
                                }
                                catch (Exception)
                                {
                                    message = "Lỗi không xác định, xin mời thao tác lại";
                                    TempData["UserMessage"] = SystemUtilites.SendSystemNotification(NotificationType.Error, message);
                                    return View(model);
                                }
                            }
                        }
                        //Add role to new user
                        result = await _userManager.AddToRoleAsync(newUser, userRole.Name);
                        if (result.Succeeded)
                        {
                            message = "Tài khoản mới đã được tạo";
                            TempData["UserMessage"] = SystemUtilites.SendSystemNotification(NotificationType.Success, message);
                            return RedirectToAction("Index");
                        }
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
                catch (Exception)
                {
                    message = "Lỗi không xác định, xin mời thao tác lại";
                    TempData["UserMessage"] = SystemUtilites.SendSystemNotification(NotificationType.Error, message);
                    return View(model);
                }

            }
            message = "Lỗi không xác định, xin mời thao tác lại";
            TempData["UserMessage"] = SystemUtilites.SendSystemNotification(NotificationType.Error, message);
            return View(model);
        }

        public async Task<IActionResult> Delete(string userId)
        {
            string message = String.Empty;
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                user.IsActive = false;
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    message = $"Đã xóa tài khoản {user.FullName}";
                    TempData["UserMessage"] = SystemUtilites.SendSystemNotification(NotificationType.Success, message);
                    return RedirectToAction("Index");
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

    }
}
