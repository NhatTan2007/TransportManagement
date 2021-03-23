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
        private decimal _cargoTonnage;
        private decimal _advanceMoney;
        private decimal _returnOfAdvances;
        private bool _isCompleted;
        private double _dateCompletedUTC;
        private double _dateCompletedLocal;
        private bool _isCancel;
        private string _reasonCancel;
        private double _dateStartUTC;
        private double _dateStartLocal;
        private string _userCreateId;
        private string _note;
        private string _timeZone;
        [Key]
        public string TransportId { get => _transportId; set => _transportId = value; }
        [MaxLength(200)]
        public string CargoTypes { get => _cargoTypes; set => _cargoTypes = value; }
        [Range(0, Int16.MaxValue)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal CargoTonnage { get => _cargoTonnage; set => _cargoTonnage = value; }
        [Column(TypeName = "decimal(18, 0)")]
        [Range(0, Int32.MaxValue)]
        public decimal AdvanceMoney { get => _advanceMoney; set => _advanceMoney = value; }
        [Range(0, Int32.MaxValue)]
        [Column(TypeName = "decimal(18, 0)")]
        public decimal ReturnOfAdvances { get => _returnOfAdvances; set => _returnOfAdvances = value; }
        public bool IsCompleted { get => _isCompleted; set => _isCompleted = value; }
        public double DateCompletedUTC { get => _dateCompletedUTC; set => _dateCompletedUTC = value; }
        public double DateCompletedLocal { get => _dateCompletedLocal; set => _dateCompletedLocal = value; }
        public bool IsCancel { get => _isCancel; set => _isCancel = value; }
        [MaxLength(200)]
        public string ReasonCancel { get => _reasonCancel; set => _reasonCancel = value; }
        [Required]
        public double DateStartUTC { get => _dateStartUTC; set => _dateStartUTC = value; }
        [Required]
        public double DateStartLocal { get => _dateStartLocal; set => _dateStartLocal = value; }
        [Required]
        [MaxLength(length:100)]
        public string TimeZone { get => _timeZone; set => _timeZone = value; }
        [MaxLength(1000)]
        public string Note { get => _note; set => _note = value; }

        //Foregin Key area
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
        public ICollection<EditTransportInformation> ListEdit { get; set; }
        public string UserCreateId { get => _userCreateId; set => _userCreateId = value; }
        [ForeignKey("UserCreateId")]
        public AppIdentityUser UserCreate { get; set; }
    }
}
