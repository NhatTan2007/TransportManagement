using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TransportManagement.Entities
{
    public class TransportInformation
    {
        private string _transportId;
        private string _cargoTypes;
        private int _cargoTonnage;
        private int _advanceMoney;
        private int _returnOfAdvances;
        private bool _isCompleted;
        private string _timeCompleted;
        private bool _isCancel;
        private string _reasonCancel;
        [Key]
        public string TransportId { get => _transportId; set => _transportId = value; }
        [Required]
        [MaxLength(200)]
        public string CargoTypes { get => _cargoTypes; set => _cargoTypes = value; }
        [Range(0, Int16.MaxValue)]
        public int CargoTonnage { get => _cargoTonnage; set => _cargoTonnage = value; }
        [Range(0, Int32.MaxValue)]
        public int AdvanceMoney { get => _advanceMoney; set => _advanceMoney = value; }
        [Range(0, Int32.MaxValue)]
        public int ReturnOfAdvances { get => _returnOfAdvances; set => _returnOfAdvances = value; }
        public bool IsCompleted { get => _isCompleted; set => _isCompleted = value; }
        public string TimeCompleted { get => _timeCompleted; set => _timeCompleted = value; }
        public bool IsCancel { get => _isCancel; set => _isCancel = value; }
        public string ReasonCancel { get => _reasonCancel; set => _reasonCancel = value; }
        [Required]
        public string DayJobId { get; set; }
        [ForeignKey("DayJobId")]
        public DayJob DayJob { get; set; }
        [Required]
        public string VehicleLicensePlate { get; set; }
        [ForeignKey("VehicleLicensePlate")]
        public Vehicle Vehicle { get; set; }
        [Required]
        public string RouteId { get; set; }
        [ForeignKey("RouteId")]
        public RouteInformation Route { get; set; }
    }
}
