using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TransportManagement.Models.Account
{
    public class AccountViewModel
    {
        private string _accountId;
        private string _userName;
        private string _firstName;
        private string _middleName;
        private string _lastName;
        private string _avatar;
        private string _email;
        private string _phoneNumber;
        [Display(Name = "Tên đăng nhập (Username)")]
        public string UserName { get => _userName; set => _userName = value; }
        [Display(Name = "Tên")]
        public string FirstName { get => _firstName; set => _firstName = value; }
        public string MiddleName { get => _middleName; set => _middleName = value; }
        [Display(Name = "Họ")]
        public string LastName { get => _lastName; set => _lastName = value; }
        public string Avatar { get => _avatar; set => _avatar = value; }
        public string Email { get => _email; set => _email = value; }
        [Display(Name = "Điện thoại di động")]
        public string PhoneNumber { get => _phoneNumber; set => _phoneNumber = value; }
        public string FullName => $"{_lastName} {_firstName}";
        public string AccountId { get => _accountId; set => _accountId = value; }
    }
}
