using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TransportManagement.Models.VehicleBrand
{
    public class CreateVehicleBrandViewModel
    {
        private string _brandName;
        [Required(ErrorMessage = "Tên thương hiệu xe không được để trống")]
        [Display(Name = "Thương hiệu xe")]
        [MaxLength(30, ErrorMessage = "Độ dài cho phép là 30 ký tự")]
        public string BrandName { get => _brandName; set => _brandName = value; }
    }
}
