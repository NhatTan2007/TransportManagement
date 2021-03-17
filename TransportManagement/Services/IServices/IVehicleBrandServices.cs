using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransportManagement.Models.VehicleBrand;
using TransportManagement.Entities;

namespace TransportManagement.Services.IServices
{
    public interface IVehicleBrandServices
    {
        public ICollection<VehicleBrandViewModel> GetAllBrands();
        public ICollection<VehicleBrandViewModel> GetAllBrands(int page, int pageSize, string search);
        public ICollection<VehicleBrandViewModel> GetAllBrands(int page, int pageSize);
        public int CountBrands();
        public Task<bool> CreateBrand(VehicleBrand newBrand);
        public Task<bool> DeleteBrand(VehicleBrand brand);
        public VehicleBrand GetBrand(string brandId);
        public Task<bool> EditBrand(EditVehicleBrandViewModel model);
    }
}
