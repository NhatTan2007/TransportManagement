using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransportManagement.Entities;

namespace TransportManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly SignInManager<AppIdentityUser> _signInManager;
        private readonly UserManager<AppIdentityUser> _userManager;

        public HomeController(UserManager<AppIdentityUser> userManager,
                                SignInManager<AppIdentityUser> signInManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null && await _userManager.IsInRoleAsync(user, "Lái xe"))
            {
                return RedirectToAction(actionName: "Index", controllerName: "Home", new {area = "Driver"});
            }
            if (user == null)
            {
                return RedirectToAction(actionName: "Login", controllerName: "Account");
            }
            return View();
        }
    }
}
