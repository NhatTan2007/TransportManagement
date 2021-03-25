using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransportManagement.DbContexts;
using TransportManagement.Entities;
using TransportManagement.Models.User;
using TransportManagement.Services.IServices;

namespace TransportManagement.Services.ImplementServices
{
    public class UserServices : IUserServices
    {
        private readonly TransportDbContext _context;
        private readonly UserManager<AppIdentityUser> _userManager;

        public UserServices(TransportDbContext context,
                                UserManager<AppIdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public int CountActiveAndAvailableUsers()
        {
            return _context.Users.Where(u => u.IsActive && u.IsAvailable).Count();
        }

        public int CountActiveUsers()
        {
            return _context.Users.Where(u => u.IsActive).Count();
        }

        public int CountAvailableUsers()
        {
            return _context.Users.Where(u => u.IsAvailable).Count();
        }

        public int CountUsers()
        {
            return _context.Users.Count();
        }

        public ICollection<UserViewModel> GetAllUsers()
        {
            return _context.Users.OrderBy(u => u.FirstName)
                                    .Select(e => new UserViewModel 
                                                    {
                                                        FullName = $"{e.LastName} {e.FirstName}",
                                                        IsAvalable = e.IsAvailable,
                                                        JobTitle = e.JobTitle,
                                                        PhoneNumber = e.PhoneNumber,
                                                        UserId = e.Id,
                                                        IsActive = e.IsActive
                                                    }).ToList();
        }

        public ICollection<UserViewModel> GetAllUsers(int page, int pageSize, int userRolePriority, string search)
        {
            return _context.Users.Where(u => u.FirstName.Contains(search) || u.LastName.Contains(search))
                                    .OrderBy(u => u.FirstName)
                                    .Skip((page - 1) * pageSize)
                                    .Take(pageSize)
                                    .Select(u => new UserViewModel
                                                        {
                                                            FullName = $"{u.LastName} {u.FirstName}",
                                                            IsAvalable = u.IsAvailable,
                                                            JobTitle = u.JobTitle,
                                                            PhoneNumber = u.PhoneNumber,
                                                            UserId = u.Id,
                                                            IsActive = u.IsActive
                                                        }).ToList();
        }

        public ICollection<UserViewModel> GetAllUsers(int page, int pageSize, int userRolePriority)
        {
            return _context.Users.Where(u => u.RolePriority > userRolePriority)
                                    .OrderBy(u => u.FirstName)
                                    .Skip((page - 1) * pageSize)
                                    .Take(pageSize)
                                    .Select(e => new UserViewModel
                                    {
                                        FullName = $"{e.LastName} {e.FirstName}",
                                        IsAvalable = e.IsAvailable,
                                        JobTitle = e.JobTitle,
                                        PhoneNumber = e.PhoneNumber,
                                        UserId = e.Id,
                                        IsActive = e.IsActive
                                    }).ToList();
        }

        public ICollection<AppIdentityUser> GetAvailableUsers()
        {
            return _context.Users.Where(u => u.IsActive == true && u.IsAvailable == true)
                                    .OrderBy(u => u.FirstName).ToList();
        }
        public ICollection<AppIdentityUser> GetDriverActiveUsers()
        {
            return _context.Users.Where(u => u.IsActive == true && u.JobTitle == "Lái xe")
                                    .OrderBy(u => u.FirstName).ToList();
        }

        public ICollection<AppIdentityUser> GetDriverUsers()
        {
            return _context.Users.Where(u => u.JobTitle == "Lái xe")
                                    .OrderBy(u => u.FirstName).ToList();
        }

        public UserViewModel GetUser(string userId)
        {
            return _context.Users.Where(u => u.Id == userId)
                                    .Select(u => new UserViewModel
                                                        {
                                                            FullName = $"{u.LastName} {u.FirstName}",
                                                            IsAvalable = u.IsAvailable,
                                                            JobTitle = u.JobTitle,
                                                            PhoneNumber = u.PhoneNumber,
                                                            UserId = u.Id,
                                                            IsActive = u.IsActive
                                                        }).SingleOrDefault();
        }

        public ICollection<UserViewModel> GetAllActiveUsers(int page, int pageSize, int userRolePriority, string search)
        {
            return _context.Users.Where(u => u.RolePriority > userRolePriority 
                                        && (u.FirstName.Contains(search) || u.LastName.Contains(search)) 
                                        && u.IsActive == true)
                                    .OrderBy(u => u.FirstName)
                                    .Skip((page - 1) * pageSize)
                                    .Take(pageSize)
                                    .Select(u => new UserViewModel
                                    {
                                        FullName = $"{u.LastName} {u.FirstName}",
                                        IsAvalable = u.IsAvailable,
                                        JobTitle = u.JobTitle,
                                        PhoneNumber = u.PhoneNumber,
                                        UserId = u.Id,
                                        IsActive = u.IsActive
                                    }).ToList();
        }

        public ICollection<UserViewModel> GetAllActiveUsers(int page, int pageSize, int userRolePriority)
        {
            return _context.Users.Where(u => u.RolePriority > userRolePriority && u.IsActive == true)
                                    .OrderBy(u => u.FirstName)
                                    .Skip((page - 1) * pageSize)
                                    .Take(pageSize)
                                    .Select(e => new UserViewModel
                                    {
                                        FullName = $"{e.LastName} {e.FirstName}",
                                        IsAvalable = e.IsAvailable,
                                        JobTitle = e.JobTitle,
                                        PhoneNumber = e.PhoneNumber,
                                        UserId = e.Id,
                                        IsActive = e.IsActive
                                    }).ToList();
        }

        public async Task<bool> MakeDriverIsBusy(AppIdentityUser driver)
        {
            try
            {
                driver.IsAvailable = false;
                var result = await _userManager.UpdateAsync(driver);
                if (result.Succeeded)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> MakeDriverIsFree(AppIdentityUser driver)
        {
            try
            {
                driver.IsAvailable = true;
                var result = await _userManager.UpdateAsync(driver);
                if (result.Succeeded)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public ICollection<AppIdentityUser> GetDriverActiveBusyUsers()
        {
            return _context.Users.Where(u => u.IsActive == true && !u.IsAvailable && u.JobTitle == "Lái xe")
                        .OrderBy(u => u.FirstName).ToList();
        }
    }
}
