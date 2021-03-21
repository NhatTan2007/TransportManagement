﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TransportManagement.Entities
{
    public class Vehicle
    {
        private int _vehicleId;
        private string _licensePlate;
        private string _vehicleName;
        private int _fuelConsumptionPerTone;
        private int _vehiclePayload;
        private string _specifications;
        private bool _isInUse;
        private bool _isAvailable;
        private bool _isDeleted;
        [Key]
        public int VehicleId { get => _vehicleId; set => _vehicleId = value; }
        [Required]
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
        public int FuelConsumptionPerTone { get => _fuelConsumptionPerTone; set => _fuelConsumptionPerTone = value; }
        [Required]
        [Range(0, Int16.MaxValue)]
        public int VehiclePayload { get => _vehiclePayload; set => _vehiclePayload = value; }
        [Required]
        public bool IsInUse { get => _isInUse; set => _isInUse = value; }
        [Required]
        public bool IsAvailable { get => _isAvailable; set => _isAvailable = value; }
        [MaxLength(1500)]
        public string Specifications { get => _specifications; set => _specifications = value; }
        [MaxLength(10)]
        public bool IsDeleted { get => _isDeleted; set => _isDeleted = value; }
        //Foreign key area
        public ICollection<TransportInformation> Transports { get; set; }
        
    }
}
