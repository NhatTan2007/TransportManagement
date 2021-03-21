using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TransportManagement.Models.Role
{
    public class RoleViewModel
    {
        private string _roleId;
        private string _roleName;
        private byte _rolePriority;
        private bool _isActive;

        public string RoleName { get => _roleName; set => _roleName = value; }
        public byte RolePriority { get => _rolePriority; set => _rolePriority = value; }
        public string RoleId { get => _roleId; set => _roleId = value; }
        public bool IsActive { get => _isActive; set => _isActive = value; }
    }
}
