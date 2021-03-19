using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransportManagement.Entities;
using TransportManagement.Models.Vehicle;

namespace TransportManagement.Services.IServices
{
    public interface IVehicleServices
    {
        public ICollection<VehicleViewModel> GetAllVehicles();
        public ICollection<VehicleViewModel> GetNotUseVehicles();
        public ICollection<VehicleViewModel> GetInUseVehicles();
        public ICollection<VehicleViewModel> GetAllVehicles(int page, int pageSize, string search);
        public ICollection<VehicleViewModel> GetAllVehicles(int page, int pageSize);
        public int CountVehicles();
        public Task<bool> CreateVehicle (Vehicle newVehicle);
        public Task<bool> DeleteVehicle(Vehicle vehicle);
        public Vehicle GetVehicle(int vehicleId);
        public Task<bool> EditVehicle(EditVehicleViewModel model);

        string IsVehicleInUsedByAnotherDriver(string driverId, int vehicleId, double TStoday);
    }
}
