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
        private string _dateCompleted;
        private bool _isCancel;
        private string _reasonCancel;
        private string _dateStart;
        private string _note;
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
        [MaxLength(12)]
        public string dateCompleted { get => _dateCompleted; set => _dateCompleted = value; }
        public bool IsCancel { get => _isCancel; set => _isCancel = value; }
        [MaxLength(200)]
        public string ReasonCancel { get => _reasonCancel; set => _reasonCancel = value; }
        [Required]
        [MaxLength(12)]
        public string DateStart { get => _dateStart; set => _dateStart = value; }
        [Required]
        public string DayJobId { get; set; }
        [ForeignKey("DayJobId")]
        public DayJob DayJob { get; set; }
        [Required]
        public int VehicleId { get; set; }
        [ForeignKey("VehicleId")]
        public Vehicle Vehicle { get; set; }
        [Required]
        public string RouteId { get; set; }
        [ForeignKey("RouteId")]
        public RouteInformation Route { get; set; }
        [MaxLength(1000)]
        public string Note { get => _note; set => _note = value; }
    }
}
