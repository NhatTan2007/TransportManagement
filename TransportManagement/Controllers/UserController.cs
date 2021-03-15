using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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

        public UserController(UserManager<AppIdentityUser> userManager,
                                SignInManager<AppIdentityUser> signInManager,
                                    IUserServices userServices)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userServices = userServices;
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
        public IActionResult Create(CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
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
                    PhoneNumber = model.PhoneNumber
                };
                if (model.Avatar != null)
                {
                    newUser.Avatar = model.Avatar.FileName;
                }
                else
                {
                    newUser.Avatar = "noavatar.png";
                }
                var result = Task.Run(async () => await _userManager.CreateAsync(newUser)).Result;
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", controllerName: "Home"); //Nên tạo 1 trang thông báo success
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }
    }
}
