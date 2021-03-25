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
        Task<ICollection<VehicleViewModel>> GetAllVehicles();
        Task<ICollection<VehicleViewModel>> GetAllNotDeletedVehicles();
        Task<ICollection<VehicleViewModel>> GetNotUseVehicles();
        Task<ICollection<VehicleViewModel>> GetInUseVehicles();
        Task<ICollection<VehicleViewModel>> GetAllNotDeletedAndAvailableVehicles();
        Task<ICollection<VehicleViewModel>> GetAllVehicles(int page, int pageSize, string search);
        Task<ICollection<VehicleViewModel>> GetAllVehicles(int page, int pageSize);
        Task<int> CountVehicles();
        Task<bool> CreateVehicle (Vehicle newVehicle);
        Task<bool> DeleteVehicle(int vehicleId);
        Task<bool> DeleteVehicleDB(Vehicle vehicle);
        Task<Vehicle> GetVehicle(int vehicleId);
        Task<bool> EditVehicle(EditVehicleViewModel model);
        Task<string> IsVehicleInUsedByAnotherDriver(string driverId, int vehicleId);
        Task<bool> MakeVehicleInUsed(Vehicle vehicle);
        Task<bool> MakeVehicleIsFree(Vehicle vehicle);
    }
}
