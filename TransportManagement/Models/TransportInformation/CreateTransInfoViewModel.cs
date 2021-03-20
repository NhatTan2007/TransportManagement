using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TransportManagement.Entities;
using TransportManagement.Models.Route;
using TransportManagement.Models.Vehicle;

namespace TransportManagement.Models.TransportInformation
{
    public class CreateTransInfoViewModel
    {
        private string _cargoTypes;
        private decimal _advanceMoney;
        private DateTime _dateStart;
        private string _note;
        private int _vehicleId;
        private string _routeId;
        private string _driverId;
        [MaxLength(200)]
        [Display(Name = "Loại hàng hóa")]
        public string CargoTypes { get => _cargoTypes; set => _cargoTypes = value; }
        [Required(ErrorMessage = "Nếu không tạm ứng thì điền 0")]
        [Display(Name = "Số tiền tạm ứng (VNĐ)")]
        [Range(typeof(decimal),"0", "2000000000", ErrorMessage = "Số tiền tạm ứng tối đa không quá 2 tỷ VNĐ")]
        public decimal AdvanceMoney { get => _advanceMoney; set => _advanceMoney = value; }
        [Display(Name = "Ngày bắt đầu")]
        public DateTime DateStart { get => _dateStart; set => _dateStart = value; }
        [Display(Name = "Ghi chú")]
        public string Note { get => _note; set => _note = value; }
        [Display(Name = "Lựa chọn phương tiện")]
        public int VehicleId { get => _vehicleId; set => _vehicleId = value; }
        [Display(Name = "Lựa chọn tuyến đường vận chuyển")]
        public string RouteId { get => _routeId; set => _routeId = value; }
        [Display(Name = "Lựa chọn tài xế")]
        public string DriverId { get => _driverId; set => _driverId = value; }
        public List<AppIdentityUser> Drivers { get; set; }
        public List<RouteViewModel> Routes { get; set; }
        public List<VehicleViewModel> Vehicles { get; set; }
    }
}
