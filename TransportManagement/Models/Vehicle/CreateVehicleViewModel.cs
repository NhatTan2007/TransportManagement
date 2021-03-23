using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TransportManagement.Models.Fuel;
using TransportManagement.Models.VehicleBrand;

namespace TransportManagement.Models.Vehicle
{
    public class CreateVehicleViewModel
    {
        private string _licensePlate;
        private string _vehicleName;
        private decimal _fuelConsumptionPerTone;
        private decimal _vehiclePayload;
        private string _specifications;
        private bool _isInUse;
        private bool _isAvailable;
        private int _fuelId;
        [Required(ErrorMessage = "Biển số phương tiện không được để trống")]
        [MaxLength(15, ErrorMessage = "Độ dài tối đa cho phép là 15 ký tự")]
        [Display(Name = "Biển số phương tiện")]
        public string LicensePlate { get => _licensePlate; set => _licensePlate = value; }
        [Required(ErrorMessage = "Tên phương tiện không được để trống")]
        [Display(Name = "Tên phương tiện")]
        [MaxLength(100, ErrorMessage = "Độ dài tối đa cho phép là 100 ký tự")]
        public string VehicleName { get => _vehicleName; set => _vehicleName = value; }
        [Required(ErrorMessage = "Định mức tiêu thụ nhiên liệu không được bỏ trống")]
        [Range(typeof(decimal),"0.1","32767", ErrorMessage = "Giá trị không đúng, xin mời nhập lại")]
        [Display(Name = "Định mức nhiên liệu/tấn hàng hóa (lít/km)")]
        public decimal FuelConsumptionPerTone { get => _fuelConsumptionPerTone; set => _fuelConsumptionPerTone = value; }
        [Required(ErrorMessage = "Trọng tải hàng hóa tối đa không được bỏ trống")]
        [Range(typeof(decimal), "0.1", "32767", ErrorMessage = "Giá trị không đúng, xin mời nhập lại")]
        [Display(Name = "Trọng tải hàng hóa tối đa (tấn)")]
        public decimal VehiclePayload { get => _vehiclePayload; set => _vehiclePayload = value; }
        [Required(ErrorMessage = "Nhãn hiệu phương tiện không được để trống")]
        [Display(Name = "Nhãn hiệu phương tiện")]
        public string VehicleBrandId { get; set; }
        [MaxLength(1500, ErrorMessage = "Thông số ký thuật tối đa cho phép 1.500 ký tự")]
        [Display(Name = "Thông số kỹ thuật")]
        public string Specifications { get => _specifications; set => _specifications = value; }
        [Display(Name = "Đang sử dụng?")]
        public bool IsInUse { get => _isInUse; set => _isInUse = value; }
        [Display(Name = "Đang sở hữu?")]
        public bool IsAvailable { get => _isAvailable; set => _isAvailable = value; }
        public List<VehicleBrandViewModel> VehicleBrands { get; set; }
        public List<FuelViewModel> Fuels { get; set; }
        [Display(Name = "Nhiên liệu sử dụng")]
        public int FuelId { get => _fuelId; set => _fuelId = value; }
    }
}
