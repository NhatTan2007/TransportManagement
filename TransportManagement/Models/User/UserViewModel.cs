using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TransportManagement.Models.User
{
    public class UserViewModel
    {
        private string _userId;
        private string _fullName;
        private string _jobTitle;
        private string _phoneNumber;
        private bool _isAvalable;
        private bool _isActive;
        public string UserId { get => _userId; set => _userId = value; }
        public string FullName { get => _fullName; set => _fullName = value; }
        public string JobTitle { get => _jobTitle; set => _jobTitle = value; }
        public string PhoneNumber { get => _phoneNumber; set => _phoneNumber = value; }
        public bool IsAvalable { get => _isAvalable; set => _isAvalable = value; }
        public bool IsActive { get => _isActive; set => _isActive = value; }
    }
}
