using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TransportManagement.Models.Vehicle
{
    public class VehicleViewModel
    {
        private int _vehicleId;
        private string _licensePlate;
        private string _vehicleName;
        private string _brandId;
        private string _brandName;
        private decimal _vehiclePayload;
        private bool _isInUse;
        private bool _isAvailable;

        public int VehicleId { get => _vehicleId; set => _vehicleId = value; }
        public string LicensePlate { get => _licensePlate; set => _licensePlate = value; }
        public string VehicleName { get => _vehicleName; set => _vehicleName = value; }
        public decimal VehiclePayload { get => _vehiclePayload; set => _vehiclePayload = value; }
        public bool IsInUse { get => _isInUse; set => _isInUse = value; }
        public bool IsAvailable { get => _isAvailable; set => _isAvailable = value; }
        public string BrandName { get => _brandName; set => _brandName = value; }
        public string BrandId { get => _brandId; set => _brandId = value; }
    }
}
