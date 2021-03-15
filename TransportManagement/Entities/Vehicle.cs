using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TransportManagement.Entities
{
    public class Vehicle
    {
        private string _licensePlate;
        private string _vehicleName;
        private int _fuleConsumptionPerTone;
        private int _vehiclePayload;
        private bool _isInUse;
        private bool _isAvailable;
        [Key]
        public string LicensePlate { get => _licensePlate; set => _licensePlate = value; }
        [Required]
        [MaxLength(200)]
        public string VehicleName { get => _vehicleName; set => _vehicleName = value; }
        [Required]
        public string VehicleBrandId { get; set; }
        [ForeignKey("VehicleBrandId")]
        public VehicleBrand Brand { get; set; }
        [Required]
        [Range(0, Int16.MaxValue)]
        public int FuleConsumptionPerTone { get => _fuleConsumptionPerTone; set => _fuleConsumptionPerTone = value; }
        [Required]
        [Range(0, Int16.MaxValue)]
        public int VehiclePayload { get => _vehiclePayload; set => _vehiclePayload = value; }
        [Required]
        public bool IsInUse { get => _isInUse; set => _isInUse = value; }
        [Required]
        public bool IsAvailable { get => _isAvailable; set => _isAvailable = value; }
        public ICollection<TransportInformation> Transports { get; set; }

    }
}
