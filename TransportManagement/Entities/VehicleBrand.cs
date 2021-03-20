using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TransportManagement.Entities
{
    public class VehicleBrand
    {
        private string _brandId;
        private string _brandName;
        [Key]
        public string BrandId { get => _brandId; set => _brandId = value; }
        [Required]
        [MaxLength(30)]
        public string BrandName { get => _brandName; set => _brandName = value; }

        //Foregin Key area
        public ICollection<Vehicle> Vehicles { get; set; }
    }
}
