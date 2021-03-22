using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransportManagement.Entities;
using TransportManagement.Models.Account;
using TransportManagement.Utilities;

namespace TransportManagement.Areas.Driver.Controllers
{
    [Area("Driver")]
    [Authorize(Roles = "Lái xe")]
    public class AccountController : Controller
    {
        private readonly SignInManager<AppIdentityUser> _signInManager;
        private readonly UserManager<AppIdentityUser> _userManager;

        public AccountController(UserManager<AppIdentityUser> userManager,
                                    SignInManager<AppIdentityUser> signInManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var account = await _userManager.GetUserAsync(User);
            if (account != null)
            {
                AccountViewModel model = new AccountViewModel()
                {
                    AccountId = account.Id,
                    Avatar = account.Avatar,
                    Email = account.Email,
                    FirstName = account.FirstName,
                    MiddleName = account.MiddleName,
                    LastName = account.LastName,
                    PhoneNumber = account.PhoneNumber,
                    UserName = account.UserName
                };
                return View(model);
            }
            return RedirectToAction(actionName: "Login", controllerName: "Account");
        }

        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            var account = await _userManager.GetUserAsync(User);
            if (account != null)
            {
                EditAccountViewModel model = new EditAccountViewModel()
                {
                    Avatar = account.Avatar,
                    Email = account.Email,
                    FirstName = account.FirstName,
                    MiddleName = account.MiddleName,
                    LastName = account.LastName,
                    PhoneNumber = account.PhoneNumber,
                    UserName = account.UserName
                };
                return View(model);
            }
            return RedirectToAction(actionName: "Login");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditAccountViewModel model)
        {
            string message = String.Empty;
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                if (ModelState.IsValid)
                {
                    if (user.Id == model.AccountId)
                    {
                        if (!String.IsNullOrEmpty(model.NewPassword) && !String.IsNullOrEmpty(model.ConfirmNewPassword))
                        {
                            if (await _userManager.CheckPasswordAsync(user, model.Password))
                            {
                                var result = await _userManager.ChangePasswordAsync(user, model.Password, model.NewPassword);
                                if (result.Succeeded)
                                {
                                    message = "Mật khẩu đã được thay đổi";
                                    TempData["UserMessage"] = SystemUtilites.SendSystemNotification(NotificationType.Success, message);
                                    return RedirectToAction(actionName: "SignOut", controllerName: "Account", new { area = ""});
                                }
                                foreach (var error in result.Errors)
                                {
                                    ModelState.AddModelError("", error.Description);
                                }
                                message = "Lỗi không xác định, xin mời thao tác lại";
                                TempData["UserMessage"] = SystemUtilites.SendSystemNotification(NotificationType.Error, message);
                                return View(model);
                            }
                            message = "Chỉ được thay đổi mật khẩu khi cung cấp mật khẩu cũ";
                            TempData["UserMessage"] = SystemUtilites.SendSystemNotification(NotificationType.Error, message);
                            return RedirectToAction(actionName: "Profile");
                        }
                    }
                    message = "Chỉ được chỉnh sửa thông tin của chính mình";
                    TempData["UserMessage"] = SystemUtilites.SendSystemNotification(NotificationType.Error, message);
                    return RedirectToAction(actionName: "Profile");
                }
                message = "Lỗi không xác định, xin mời thao tác lại";
                TempData["UserMessage"] = SystemUtilites.SendSystemNotification(NotificationType.Error, message);
                return RedirectToAction(actionName: "Profile");

            }
            return RedirectToAction(actionName: "Login");
        }
    }
}
