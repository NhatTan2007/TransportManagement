using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransportManagement.Entities;
using TransportManagement.Models.User;

namespace TransportManagement.Services.IServices
{
    public interface IUserServices
    {
        ICollection<UserViewModel> GetAllUsers();
        ICollection<UserViewModel> GetAllUsers(int page, int pageSize, int userRolePriority, string search);
        ICollection<UserViewModel> GetAllUsers(int page, int pageSize, int userRolePriority);
        ICollection<UserViewModel> GetAllActiveUsers(int page, int pageSize, int userRolePriority, string search);
        ICollection<UserViewModel> GetAllActiveUsers(int page, int pageSize, int userRolePriority);
        int CountUsers();
        int CountActiveUsers();
        int CountAvailableUsers();
        int CountActiveAndAvailableUsers();
        ICollection<AppIdentityUser> GetAvailableUsers();
        ICollection<AppIdentityUser> GetDriverActiveUsers();
        ICollection<AppIdentityUser> GetDriverActiveBusyUsers();
        ICollection<AppIdentityUser> GetDriverUsers();
        UserViewModel GetUser(string userId);
        Task<bool> MakeDriverIsBusy(AppIdentityUser driver);
        Task<bool> MakeDriverIsFree(AppIdentityUser driver);
    }
}
