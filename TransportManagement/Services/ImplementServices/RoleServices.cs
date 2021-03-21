using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransportManagement.Entities;
using TransportManagement.Models.Role;
using TransportManagement.Services.IServices;

namespace TransportManagement.Services.ImplementServices
{
    public class RoleServices : IRoleServices
    {
        private readonly UserManager<AppIdentityUser> _userManager;
        private readonly RoleManager<AppIdentityRole> _roleManager;

        public RoleServices(UserManager<AppIdentityUser> userManager,
                                RoleManager<AppIdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<ICollection<RoleViewModel>> GetRoles(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var userRoles = await _userManager.GetRolesAsync(user);
            if (userRoles != null)
            {
                int maxRoleLevel = 0;
                foreach (var roleName in userRoles)
                {
                    var role = await _roleManager.FindByNameAsync(roleName);
                    if (maxRoleLevel < role.RolePriority) maxRoleLevel = role.RolePriority;
                }
                return _roleManager.Roles.Where(r => r.RolePriority > maxRoleLevel && r.IsActive == true)
                                            .Select(r => new RoleViewModel() 
                                                                {
                                                                    RoleId = r.Id,
                                                                    RoleName = r.Name,
                                                                    RolePriority = r.RolePriority
                                                                }).ToList();
            }
            return new List<RoleViewModel>();
            
        }

        public async Task<int> GetUserRolePriority(AppIdentityUser user)
        {
            int maxRoleLevel = -1;
            var userRoles = await _userManager.GetRolesAsync(user);
            if (userRoles != null)
            {
                
                foreach (var roleName in userRoles)
                {
                    var role = await _roleManager.FindByNameAsync(roleName);
                    if (maxRoleLevel < role.RolePriority) maxRoleLevel = role.RolePriority;
                }
            }
            return maxRoleLevel;
        }
    }
}
