using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransportManagement.Entities;
using TransportManagement.Models.Role;

namespace TransportManagement.Services.IServices
{
    public interface IRoleServices
    {
        Task<ICollection<RoleViewModel>> GetRoles(string userId);
        Task<int> GetUserRolePriority(AppIdentityUser user);
    }
}
