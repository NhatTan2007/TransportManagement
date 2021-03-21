using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TransportManagement.Entities
{
    public class AppIdentityUser : IdentityUser
    {
        private string _firstName;
        private string _middleName;
        private string _lastName;
        private string _avatar;
        private bool _isActive;
        private bool _isAvailable;
        private string _jobTitle;
        private string _department;
        private int _rolePriority;
        [Required]
        public string FirstName { get => _firstName; set => _firstName = value; }
        public string MiddleName { get => _middleName; set => _middleName = value; }
        [Required]
        public string LastName { get => _lastName; set => _lastName = value; }
        public string FullName => $"{LastName} {FirstName}";
        [Required]
        public string Avatar { get => _avatar; set => _avatar = value; }
        [Required]
        public bool IsActive { get => _isActive; set => _isActive = value; }
        [Required]
        public bool IsAvailable { get => _isAvailable; set => _isAvailable = value; }
        public string JobTitle { get => _jobTitle; set => _jobTitle = value; }
        public string Department { get => _department; set => _department = value; }
        [Required]
        public int RolePriority { get => _rolePriority; set => _rolePriority = value; }
        //Foreign Key area
        public ICollection<DayJob> DayJobs { get; set; }
        public ICollection<EditTransportInformation> ListEdit { get; set; }
        
    }
}
