using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TransportManagement.Models.VehicleBrand
{
    public class EditVehicleBrandViewModel
    {
        private string _brandId;
        private string _brandName;
        [Required(ErrorMessage = "Tên thương hiệu xe không được để trống")]
        [Display(Name = "Thương hiệu xe")]
        [MaxLength(30, ErrorMessage = "Độ dài cho phép là 30 ký tự")]
        public string BrandName { get => _brandName; set => _brandName = value; }
        public string BrandId { get => _brandId; set => _brandId = value; }
    }
}
