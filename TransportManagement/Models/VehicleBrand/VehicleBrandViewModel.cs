using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TransportManagement.Models.VehicleBrand
{
    public class VehicleBrandViewModel
    {
        private string _brandId;
        private string _brandName;

        public string BrandName { get => _brandName; set => _brandName = value; }
        public string BrandId { get => _brandId; set => _brandId = value; }
    }
}
