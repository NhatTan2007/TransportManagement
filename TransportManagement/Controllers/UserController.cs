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
using TransportManagement.Services;

namespace TransportManagement.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<AppIdentityUser> _userManager;
        private readonly SignInManager<AppIdentityUser> _signInManager;
        private readonly IUserServices _userServices;
        private readonly IWebHostEnvironment _webEnv;

        public UserController(UserManager<AppIdentityUser> userManager,
                                SignInManager<AppIdentityUser> signInManager,
                                    IUserServices userServices,
                                        IWebHostEnvironment webEnv)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userServices = userServices;
            _webEnv = webEnv;
        }
        public IActionResult Index(int page, int pageSize, string search)
        {
            int countTotalUsers = _userServices.CountUsers();
            PaginationViewModel<UserViewModel> model = new PaginationViewModel<UserViewModel>();
            if (page == 0) page = 1;
            if (pageSize == 0) pageSize = model.PageSizeItem.Min();
            model.Pager = new Pager(countTotalUsers, page, pageSize);
            if (String.IsNullOrEmpty(search))
            {
                model.Items = _userServices.GetAllUsers(page, pageSize).ToList();
            }
            else
            {
                model.Items = _userServices.GetAllUsers(page, pageSize, search).ToList();
            }
            ViewBag.Search = search;
            return View(model);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
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
                    Avatar = fileName
                };
                try
                {
                    var result = await _userManager.CreateAsync(newUser);
                    if (result.Succeeded)
                    {
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
                                    return View(model);
                                }
                            }
                        }
                        return RedirectToAction("Index", controllerName: "Home"); //Nên tạo 1 trang thông báo success
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Có lỗi xảy ra, xin mời liên hệ quản trị hệ thống");
                    return View(model);
                }

            }
            return View(model);
        }
    }
}
