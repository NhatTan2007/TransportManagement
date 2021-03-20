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
        private string _rolePriority;

        [Required]
        public string RolePriority { get => _rolePriority; set => _rolePriority = value; }
    }
}
