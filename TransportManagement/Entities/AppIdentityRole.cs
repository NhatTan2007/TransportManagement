using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TransportManagement.Entities
{
    public class AppIdentityRole : IdentityRole
    {
        private byte _rolePriority;
        private bool _isActive;

        [Required]
        public byte RolePriority { get => _rolePriority; set => _rolePriority = value; }
        [Required]
        public bool IsActive { get => _isActive; set => _isActive = value; }
    }
}
