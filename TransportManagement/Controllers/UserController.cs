using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "Quản trị viên hệ thống, Nhân sự")]
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
        [Authorize(Roles = "Quản trị viên hệ thống, Nhân sự")]
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
        [Authorize(Roles = "Quản trị viên hệ thống, Nhân sự")]
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
                    string[] typeAvatar = model.Avatar.ContentType.Split(@"\");
                    fileName = $"{Guid.NewGuid()}_{typeAvatar[typeAvatar.Length - 1]}";
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
        [HttpGet]
        [Authorize(Roles = "Quản trị viên hệ thống, Nhân sự")]
        public async Task<IActionResult> Edit(string userId)
        {
            string message = String.Empty;
            var userToEdit = await _userManager.FindByIdAsync(userId);
            if (userToEdit != null)
            {
                var user = await _userManager.GetUserAsync(User);
                var roleUserEdit = await _userManager.GetRolesAsync(userToEdit);
                var roleIdUserEdit = await _roleManager.FindByNameAsync(roleUserEdit[0]);
                EditUserViewModel model = new EditUserViewModel()
                {
                    UserId = userToEdit.Id,
                    ExistsAvatar = userToEdit.Avatar,
                    Email = userToEdit.Email,
                    FirstName = userToEdit.FirstName,
                    MiddleName = userToEdit.MiddleName,
                    LastName = userToEdit.LastName,
                    PhoneNumber = userToEdit.PhoneNumber,
                    RoleId = roleIdUserEdit.Id,
                    Roles = await _roleServices.GetRoles(user.Id)
                };
                return View(model);
            }
            message = "Lỗi không xác định, xin mời thao tác lại";
            TempData["UserMessage"] = SystemUtilites.SendSystemNotification(NotificationType.Error, message);
            return RedirectToAction(actionName: "Index");
        }

        [HttpPost]
        [Authorize(Roles = "Quản trị viên hệ thống, Nhân sự")]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            model.Roles = await _roleServices.GetRoles(user.Id);
            string message = String.Empty;
            if (ModelState.IsValid)
            {
                var userEdit = await _userManager.FindByIdAsync(model.UserId);
                if (userEdit == null)
                {
                    message = "Không tìm thấy tài khoản, xin mời thao tác lại";
                    TempData["UserMessage"] = SystemUtilites.SendSystemNotification(NotificationType.Error, message);
                    return RedirectToAction(actionName: "Index");
                }
                var roleUserEdit = await _userManager.GetRolesAsync(userEdit);
                var roleIdUserEdit = await _roleManager.FindByNameAsync(roleUserEdit[0]);
                userEdit.FirstName = model.FirstName;
                userEdit.MiddleName = model.MiddleName;
                userEdit.LastName = model.LastName;
                userEdit.Email = model.Email;
                if (model.Avatar != null)
                {
                    string folderPath = Path.Combine(_webEnv.WebRootPath, "images", "account");
                    string filePath = String.Empty;
                    if (model.ExistsAvatar != "noavatar.png")
                    {
                        filePath = Path.Combine(folderPath, model.ExistsAvatar);
                        System.IO.File.Delete(filePath);
                    }
                    string[] typeAvatar = model.Avatar.ContentType.Split(@"/");
                    var fileName = $"{Guid.NewGuid()}.{typeAvatar[typeAvatar.Length - 1]}";
                    filePath = Path.Combine(folderPath, fileName);
                    using (FileStream fs = new FileStream(filePath, FileMode.Create))
                    {
                        try
                        {
                            await model.Avatar.CopyToAsync(fs);
                            userEdit.Avatar = fileName;
                        }
                        catch (Exception)
                        {
                            message = "Lỗi không xác định, xin mời thao tác lại";
                            TempData["UserMessage"] = SystemUtilites.SendSystemNotification(NotificationType.Error, message);
                            return View(model);
                        }
                    }
                }
                if (roleIdUserEdit.Id != model.RoleId)
                {
                    var roleEdit = await _roleManager.FindByIdAsync(model.RoleId);
                    if (roleEdit != null)
                    {
                        var result = await _userManager.AddToRoleAsync(userEdit, roleEdit.Name);
                        if (result.Succeeded)
                        {
                            result = await _userManager.RemoveFromRoleAsync(userEdit, roleIdUserEdit.Name);
                        }
                    }
                }
                userEdit.PhoneNumber = model.PhoneNumber;
                if (!String.IsNullOrEmpty(model.Password))
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(userEdit);
                    var result = await _userManager.ResetPasswordAsync(userEdit, token, model.Password);
                    if (!result.Succeeded)
                    {
                        message = "Lỗi không xác định, xin mời thao tác lại";
                        TempData["UserMessage"] = SystemUtilites.SendSystemNotification(NotificationType.Error, message);
                        return View(model);
                    }
                }
                var resultEditUser = await _userManager.UpdateAsync(userEdit);
                if (resultEditUser.Succeeded)
                {
                    message = "Thông tin tài khoản đã được đổi thành công";
                    TempData["UserMessage"] = SystemUtilites.SendSystemNotification(NotificationType.Success, message);
                    return RedirectToAction(actionName: "Index");
                }
                foreach (var error in resultEditUser.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            message = "Lỗi không xác định, xin mời thao tác lại";
            TempData["UserMessage"] = SystemUtilites.SendSystemNotification(NotificationType.Error, message);
            return View(model);
        }
        [Authorize(Roles = "Quản trị viên hệ thống, Nhân sự")]
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
        [Authorize(Roles = "Quản trị viên hệ thống, Nhân sự")]
        public async Task<IActionResult> Details(string userId)
        {
            string message = String.Empty;
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var rolesUser = await _userManager.GetRolesAsync(user);
                var roleIdUser = await _roleManager.FindByNameAsync(rolesUser[0]);
                var detailUser = new DetailUserViewModel()
                {
                    UserId = user.Id,
                    Avatar = user.Avatar,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    MiddleName = user.MiddleName,
                    LastName = user.LastName,
                    PhoneNumber = user.PhoneNumber,
                    RoleId = roleIdUser.Id,
                    Roles = _roleManager.Roles.ToList(),
                };
                return View(detailUser);
            }
            message = "Không tìm thấy tài khoản, xin mời thao tác lại";
            TempData["UserMessage"] = SystemUtilites.SendSystemNotification(NotificationType.Error, message);
            return RedirectToAction(actionName: "Index");
        }
    }
}
