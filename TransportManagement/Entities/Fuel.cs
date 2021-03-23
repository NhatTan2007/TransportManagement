using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TransportManagement.Entities
{
    public class Fuel
    {
        private int _fuelId;
        private string _fuelName;
        private decimal _fuelPrice;
        [Key]
        public int FuelId { get => _fuelId; set => _fuelId = value; }
        [Required]
        [MaxLength(20)]
        public string FuelName { get => _fuelName; set => _fuelName = value; }
        [Required]
        [Column(TypeName = "decimal(18,0)")]
        public decimal FuelPrice { get => _fuelPrice; set => _fuelPrice = value; }
    }
}
