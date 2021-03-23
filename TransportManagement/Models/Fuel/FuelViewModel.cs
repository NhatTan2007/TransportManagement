using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TransportManagement.Models.Fuel
{
    public class FuelViewModel
    {
        private int _fuelId;
        private string _fuelName;
        private decimal _fuelPrice;
        [Display(Name = "Tên nhiên liệu")]
        [MaxLength(200)]
        public string FuelName { get => _fuelName; set => _fuelName = value; }
        [Column(TypeName = "decimal(18,0)")]
        [Range(typeof(decimal), "0.1", "100000", ErrorMessage = "Giá trị không phù hợp")]
        [Display(Name = "Giá nhiên liệu/lít")]
        public decimal FuelPrice { get => _fuelPrice; set => _fuelPrice = value; }
        public int FuelId { get => _fuelId; set => _fuelId = value; }
    }
}
