using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TransportManagement.Models.User
{
    public class CreateUserViewModel
    {
        private string _userName;
        private string _firstName;
        private string _middleName;
        private string _lastName;
        private IFormFile _avatar;
        private string _password;
        private string _confirmPassword;
        private string _email;
        private string _phoneNumber;
        private bool _isActive;
        private bool _isAvailable;
        [Required(ErrorMessage = "Tên đăng nhập không được để trống")]
        [MaxLength(20, ErrorMessage = ("Độ dài tối đa của tên đăng nhập là 20"))]
        [Display(Name = "Tên đăng nhập (Username)")]
        public string UserName { get => _userName; set => _userName = value; }
        [Required(ErrorMessage = "Tên không được để trống")]
        [MaxLength(20, ErrorMessage = ("Độ dài tối đa của tên là 20"))]
        [Display(Name = "Tên")]
        public string FirstName { get => _firstName; set => _firstName = value; }
        public string MiddleName { get => _middleName; set => _middleName = value; }
        [Required(ErrorMessage = "Họ không được để trống")]
        [MaxLength(50, ErrorMessage = ("Độ dài tối đa của họ là 50"))]
        [Display(Name = "Họ")]
        public string LastName { get => _lastName; set => _lastName = value; }
        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        [Display(Name = "Mật khẩu")]
        [DataType(DataType.Password)]
        public string Password { get => _password; set => _password = value; }
        [Required(ErrorMessage = "Xác nhận mật khẩu không được để trống")]
        [Compare("Password", ErrorMessage = "Mật khẩu xác nhận không chính xác")]
        [Display(Name = "Xác nhận mật khẩu")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get => _confirmPassword; set => _confirmPassword = value; }
        public IFormFile Avatar { get => _avatar; set => _avatar = value; }
        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"(^[\w])+([\w._])*\@([\w{2,}\-])+(\.[\w]{2,4})$", ErrorMessage = "Không đúng định dạng email")]
        public string Email { get => _email; set => _email = value; }
        [Required(ErrorMessage = "Số điện thoại di động không được để trống")]
        [RegularExpression(@"(^0)+(3[2-9]|5[6|8|9]|7[0|6-9]|8(?!7|0)\d|9(?!5)[\d])+([0-9]{7})", ErrorMessage = "Không đúng định dạng điện thoại di động ở VN")]
        [Display(Name = "Điện thoại di động")]
        public string PhoneNumber { get => _phoneNumber; set => _phoneNumber = value; }
        [Required(ErrorMessage = "Tình trạng hoạt động không được để trống")]
        [Display(Name = "Hoạt động")]
        public bool IsActive { get => _isActive; set => _isActive = value; }
        [Required(ErrorMessage = "Tình trạng sẵn sàng không được để trống")]
        [Display(Name = "Sẵn sàng")]
        public bool IsAvailable { get => _isAvailable; set => _isAvailable = value; }
    }
}
