using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TransportManagement.Models.Role
{
    public class CreateRoleViewModel
    {
        private string _roleName;
        private byte _rolePriority;
        [Required(ErrorMessage = "Tên phân quyền không được bỏ trống")]
        [MaxLength(30, ErrorMessage = "Tên phân quyền không được quá 30 ký tự")]
        [Display(Name = "Tên phân quyền")]
        public string RoleName { get => _roleName; set => _roleName = value; }
        [Required(ErrorMessage = "Cấp bậc phân quyền không được bỏ trống")]
        [Range(typeof(byte), "0", "20", ErrorMessage = "Cấp bậc phân quyền không được quá 20")]
        [Display(Name = "Cấp bậc phân quyền")]
        public byte RolePriority { get => _rolePriority; set => _rolePriority = value; }
    }
}
