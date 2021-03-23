using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TransportManagement.Models.TransportInformation
{
    public class TransInfoViewModel
    {
        private string _transportId;
        private string _vehicleLicensePlate;
        private string _driverName;
        private decimal _cargoTonnage;
        private decimal _advanceMoney;
        private decimal _returnOfAdvances;
        private bool _isCompleted;
        private bool _isCancel;
        private double _dateStartLocal;
        public string TransportId { get => _transportId; set => _transportId = value; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal CargoTonnage { get => _cargoTonnage; set => _cargoTonnage = value; }
        public decimal AdvanceMoney { get => _advanceMoney; set => _advanceMoney = value; }
        public decimal ReturnOfAdvances { get => _returnOfAdvances; set => _returnOfAdvances = value; }
        public bool IsCompleted { get => _isCompleted; set => _isCompleted = value; }
        public bool IsCancel { get => _isCancel; set => _isCancel = value; }
        public double DateStartLocal { get => _dateStartLocal; set => _dateStartLocal = value; }
        public string VehicleLicensePlate { get => _vehicleLicensePlate; set => _vehicleLicensePlate = value; }
        public string DriverName { get => _driverName; set => _driverName = value; }
    }
}
