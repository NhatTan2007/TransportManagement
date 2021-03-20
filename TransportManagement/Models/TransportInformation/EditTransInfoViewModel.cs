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
    public class EditTransInfoViewModel
    {
        private string _transportId;
        private string _cargoTypes;
        private int _cargoTonnage;
        private decimal _advanceMoney;
        private decimal _returnOfAdvances;
        private bool _isCompleted;
        private double _dateCompletedUTC;
        private double _dateCompletedLocal;
        private bool _isCancel;
        private string _reasonCancel;
        private int _vehicleId;
        private string _routeId;
        private string _driverId;
        private string _note;

        public string TransportId { get => _transportId; set => _transportId = value; }
        [MaxLength(200)]
        [Display(Name = "Loại hàng hóa")]
        public string CargoTypes { get => _cargoTypes; set => _cargoTypes = value; }
        [Range(0, Int16.MaxValue)]
        public int CargoTonnage { get => _cargoTonnage; set => _cargoTonnage = value; }
        [Required(ErrorMessage = "Nếu không tạm ứng thì điền 0")]
        [Display(Name = "Số tiền tạm ứng (VNĐ)")]
        [Range(typeof(decimal),"0", "2000000000", ErrorMessage = "Số tiền tạm ứng không vượt quá 2 tỷ VNĐ")]
        public decimal AdvanceMoney { get => _advanceMoney; set => _advanceMoney = value; }
        [Required(ErrorMessage = "Nếu không hoàn tạm ứng thì điền 0")]
        [Display(Name = "Số tiền hoàn tạm ứng (VNĐ)")]
        [Range(typeof(decimal), "0", "2000000000", ErrorMessage = "Số tiền hoàn tạm ứng không vượt quá 2 tỷ VNĐ")]
        public decimal ReturnOfAdvances { get => _returnOfAdvances; set => _returnOfAdvances = value; }
        [Display(Name = "Hoàn thành")]
        public bool IsCompleted { get => _isCompleted; set => _isCompleted = value; }
        [Display(Name = "Hủy")]
        public bool IsCancel { get => _isCancel; set => _isCancel = value; }
        [Display(Name = "Lý do hủy")]
        public string ReasonCancel { get => _reasonCancel; set => _reasonCancel = value; }
        [Display(Name = "Ghi chú")]
        public string Note { get => _note; set => _note = value; }
        [Display(Name = "Lựa chọn phương tiện")]
        public int VehicleId { get => _vehicleId; set => _vehicleId = value; }
        [Display(Name = "Lựa chọn tuyến đường vận chuyển")]
        public string RouteId { get => _routeId; set => _routeId = value; }
        [Display(Name = "Lựa chọn tài xế")]
        public string DriverId { get => _driverId; set => _driverId = value; }
        public double DateCompletedUTC { get => _dateCompletedUTC; set => _dateCompletedUTC = value; }
        public double DateCompletedLocal { get => _dateCompletedLocal; set => _dateCompletedLocal = value; }

        public List<AppIdentityUser> Drivers { get; set; }
        public List<RouteViewModel> Routes { get; set; }
        public List<VehicleViewModel> Vehicles { get; set; }

    }
}
