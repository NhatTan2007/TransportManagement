using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TransportManagement.Entities;

namespace TransportManagement.Models.User
{
    public class DetailUserViewModel
    {
        private string _userId;
        private string _firstName;
        private string _middleName;
        private string _lastName;
        private string _avatar;
        private string _email;
        private string _phoneNumber;
        private string _roleId;
        [Display(Name = "Tên")]
        public string FirstName { get => _firstName; set => _firstName = value; }
        public string MiddleName { get => _middleName; set => _middleName = value; }
        public string LastName { get => _lastName; set => _lastName = value; }
        public string Email { get => _email; set => _email = value; }
        [Display(Name = "Điện thoại di động")]
        public string PhoneNumber { get => _phoneNumber; set => _phoneNumber = value; }
        [Display(Name = "Phân quyền tài khoản")]
        public string RoleId { get => _roleId; set => _roleId = value; }
        public ICollection<AppIdentityRole> Roles { get; set; }
        public string Avatar { get => _avatar; set => _avatar = value; }
        public string UserId { get => _userId; set => _userId = value; }
    }
}
