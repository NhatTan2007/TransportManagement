using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TransportManagement.Entities;
using TransportManagement.Models.Role;

namespace TransportManagement.Models.User
{
    public class EditUserViewModel
    {
        private string _userId;
        private string _firstName;
        private string _middleName;
        private string _lastName;
        private string _existsAvatar;
        private IFormFile _avatar;
        private string _password;
        private string _confirmPassword;
        private string _email;
        private string _phoneNumber;
        private string _roleId;
        [Required(ErrorMessage = "Tên không được để trống")]
        [MaxLength(20, ErrorMessage = ("Độ dài tối đa của tên là 20"))]
        [Display(Name = "Tên")]
        public string FirstName { get => _firstName; set => _firstName = value; }
        public string MiddleName { get => _middleName; set => _middleName = value; }
        [Required(ErrorMessage = "Họ không được để trống")]
        [MaxLength(50, ErrorMessage = ("Độ dài tối đa của họ là 50"))]
        [Display(Name = "Họ")]
        public string LastName { get => _lastName; set => _lastName = value; }
        [Display(Name = "Mật khẩu")]
        [DataType(DataType.Password)]
        public string Password { get => _password; set => _password = value; }
        [Compare("Password", ErrorMessage = "Mật khẩu xác nhận không chính xác")]
        [Display(Name = "Xác nhận mật khẩu")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get => _confirmPassword; set => _confirmPassword = value; }
        public IFormFile Avatar { get => _avatar; set => _avatar = value; }
        [RegularExpression(@"(^[\w])+([\w._])*\@([\w{2,}\-])+(\.[\w]{2,4})$", ErrorMessage = "Không đúng định dạng email")]
        public string Email { get => _email; set => _email = value; }
        [Required(ErrorMessage = "Số điện thoại di động không được để trống")]
        [RegularExpression(@"(^0)+(3[2-9]|5[6|8|9]|7[0|6-9]|8[1-6|8-9]|9[0-4|6-9])+([0-9]{7})", ErrorMessage = "Không đúng định dạng điện thoại di động ở VN")]
        [Display(Name = "Điện thoại di động")]
        public string PhoneNumber { get => _phoneNumber; set => _phoneNumber = value; }
        [Required(ErrorMessage = "Phân quyền không được để trống")]
        [Display(Name = "Phân quyền tài khoản")]
        public string RoleId { get => _roleId; set => _roleId = value; }
        public ICollection<RoleViewModel> Roles { get; set; }
        public string ExistsAvatar { get => _existsAvatar; set => _existsAvatar = value; }
        public string UserId { get => _userId; set => _userId = value; }
    }
}
