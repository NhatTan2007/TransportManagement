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

        public UserServices(TransportDbContext context)
        {
            _context = context;
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
            return _context.Users.Select(e => new UserViewModel {
                                                            FullName = $"{e.LastName} {e.FirstName}",
                                                            IsAvalable = e.IsAvailable,
                                                            JobTitle = e.JobTitle,
                                                            PhoneNumber = e.PhoneNumber,
                                                            UserId = e.Id
                                                            }).ToList();
        }

        public ICollection<UserViewModel> GetAllUsers(int page, int pageSize, string search)
        {
            return _context.Users.Where(u => u.FirstName.Contains(search) || u.LastName.Contains(search))
                                    .Skip((page - 1) * pageSize)
                                        .Take(pageSize)
                                            .Select(u => new UserViewModel
                                                                {
                                                                    FullName = $"{u.LastName} {u.FirstName}",
                                                                    IsAvalable = u.IsAvailable,
                                                                    JobTitle = u.JobTitle,
                                                                    PhoneNumber = u.PhoneNumber,
                                                                    UserId = u.Id
                                                                }).ToList();
        }

        public ICollection<UserViewModel> GetAllUsers(int page, int pageSize)
        {
            return _context.Users.Skip((page - 1) * pageSize)
                                    .Take(pageSize)
                                        .Select(e => new UserViewModel
                                        {
                                            FullName = $"{e.LastName} {e.FirstName}",
                                            IsAvalable = e.IsAvailable,
                                            JobTitle = e.JobTitle,
                                            PhoneNumber = e.PhoneNumber,
                                            UserId = e.Id
                                        }).ToList();
        }

        public ICollection<AppIdentityUser> GetAvailableUsers()
        {
            return _context.Users.Where(u => u.IsActive == true && u.IsAvailable == true).ToList();
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
                                                            UserId = u.Id
                                                        }).SingleOrDefault();
        }
    }
}
