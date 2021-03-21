using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TransportManagement.Models.Account
{
    public class LoginAccountViewModel
    {
        private string _userName;
        private string _password;
        private bool _isRemember;
        [Display(Name = "Tên đăng nhập")]
        [Required(ErrorMessage = "Tên đăng nhập không được để trống")]
        public string UserName { get => _userName; set => _userName = value; }
        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        [DataType(DataType.Password)]
        public string Password { get => _password; set => _password = value; }
        [Display(Name = "Ghi nhớ tài khoản")]
        public bool IsRemember { get => _isRemember; set => _isRemember = value; }
    }
}
