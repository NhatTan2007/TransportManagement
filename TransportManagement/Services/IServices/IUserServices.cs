using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransportManagement.Models.User;

namespace TransportManagement.Services
{
    public interface IUserServices
    {
        public ICollection<UserViewModel> GetAllUsers();
        public ICollection<UserViewModel> GetAllUsers(int page, int pageSize, string search);
        public ICollection<UserViewModel> GetAllUsers(int page, int pageSize);
        public int CountUsers();
        public int CountActiveUsers();
        public int CountAvailableUsers();
        public int CountActiveAndAvailableUsers();
    }
}
