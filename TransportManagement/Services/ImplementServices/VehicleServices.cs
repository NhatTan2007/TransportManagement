using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransportManagement.DbContexts;
using TransportManagement.Entities;
using TransportManagement.Models.Vehicle;
using TransportManagement.Services.IServices;

namespace TransportManagement.Services.ImplementServices
{
    public class VehicleServices : IVehicleServices
    {
        private readonly TransportDbContext _context;
        private readonly ITransInfoServices _transInfoServices;

        public VehicleServices(TransportDbContext context,
                                ITransInfoServices transInfoServices)
        {
            _context = context;
            _transInfoServices = transInfoServices;
        }
        public int CountVehicles()
        {
            return _context.Vehicles.Count();
        }

        public async Task<bool> CreateVehicle(Vehicle newVehicle)
        {
            try
            {
                _context.Vehicles.Add(newVehicle);
                var result = await _context.SaveChangesAsync();
                return result > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteVehicle(int vehicleId)
        {
            var vehicle = await _context.Vehicles.Where(v => v.VehicleId == vehicleId).SingleOrDefaultAsync();
            if (vehicle != null)
            {
                _context.Vehicles.Attach(vehicle);
                vehicle.IsDeleted = true;
                var result = await _context.SaveChangesAsync();
                return result > 0;
            }
            return false;
        }

        public async Task<bool> DeleteVehicleDB(Vehicle vehicle)
        {
            try
            {
                _context.Vehicles.Remove(vehicle);
                var result = await _context.SaveChangesAsync();
                return result > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> EditVehicle(EditVehicleViewModel model)
        {
            try
            {
                var vehicle = await GetVehicle(model.VehicleId);
                if (vehicle != null)
                {
                    if (!vehicle.IsDeleted)
                    {
                        _context.Vehicles.Attach(vehicle);
                        vehicle.VehicleName = model.VehicleName;
                        vehicle.FuelConsumptionPerTone = model.FuelConsumptionPerTone;
                        vehicle.IsAvailable = model.IsAvailable;
                        vehicle.IsInUse = model.IsInUse;
                        vehicle.VehicleBrandId = model.VehicleBrandId;
                        vehicle.Specifications = model.Specifications;
                        vehicle.VehiclePayload = model.VehiclePayload;
                        var result = await _context.SaveChangesAsync();
                        return result > 0;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
            return false;
        }

        public ICollection<VehicleViewModel> GetAllVehicles()
        {
            return _context.Vehicles.Where(v => v.IsDeleted == false)
                                    .Include(v => v.Brand)
                                    .OrderBy(v => v.Brand.BrandName)
                                    .Select(v => new VehicleViewModel()
                                    {
                                        VehicleId = v.VehicleId,
                                        LicensePlate = v.LicensePlate,
                                        IsAvailable = v.IsAvailable,
                                        BrandName = v.Brand.BrandName,
                                        IsInUse = v.IsInUse,
                                        VehicleName = v.VehicleName,
                                        VehiclePayload = v.VehiclePayload
                                    }).ToList();
        }

        public ICollection<VehicleViewModel> GetAllVehicles(int page, int pageSize, string search)
        {
            return _context.Vehicles.Where(v => v.LicensePlate.Contains(search) && v.IsDeleted == false)
                                    .Include(v => v.Brand)
                                    .OrderBy(v => v.Brand.BrandName)
                                    .Skip((page - 1) * pageSize)
                                    .Take(pageSize)
                                    .Select(v => new VehicleViewModel()
                                    {
                                        VehicleId = v.VehicleId,
                                        LicensePlate = v.LicensePlate,
                                        IsAvailable = v.IsAvailable,
                                        BrandName = v.Brand.BrandName,
                                        IsInUse = v.IsInUse,
                                        VehicleName = v.VehicleName,
                                        VehiclePayload = v.VehiclePayload
                                    }).ToList();
        }

        public ICollection<VehicleViewModel> GetAllVehicles(int page, int pageSize)
        {
            return _context.Vehicles.Where(v => v.IsDeleted == false)
                                    .Include(v => v.Brand)
                                    .OrderBy(v => v.Brand.BrandName)
                                    .Skip((page - 1) * pageSize)
                                    .Take(pageSize)
                                    .Select(v => new VehicleViewModel()
                                    {
                                        VehicleId = v.VehicleId,
                                        LicensePlate = v.LicensePlate,
                                        IsAvailable = v.IsAvailable,
                                        BrandName = v.Brand.BrandName,
                                        IsInUse = v.IsInUse,
                                        VehicleName = v.VehicleName,
                                        VehiclePayload = v.VehiclePayload
                                    }).ToList();
        }

        public ICollection<VehicleViewModel> GetInUseVehicles()
        {
            return _context.Vehicles.Where(v => v.IsDeleted == false)
                                    .Include(v => v.Brand)
                                    .OrderBy(v => v.Brand.BrandName)
                                    .Where(v => v.IsInUse == true && v.IsAvailable == true)
                                    .Select(v => new VehicleViewModel()
                                                    {
                                                        VehicleId = v.VehicleId,
                                                        LicensePlate = v.LicensePlate,
                                                        IsAvailable = v.IsAvailable,
                                                        BrandName = v.Brand.BrandName,
                                                        IsInUse = v.IsInUse,
                                                        VehicleName = v.VehicleName,
                                                        VehiclePayload = v.VehiclePayload
                                                    }).ToList();
        }

        public ICollection<VehicleViewModel> GetNotUseVehicles()
        {
            return _context.Vehicles.Where(v => v.IsDeleted == false)
                                    .Include(v => v.Brand)
                                    .OrderBy(v => v.Brand.BrandName)
                                    .Where(v => v.IsInUse == false && v.IsAvailable == true)
                                    .Select(v => new VehicleViewModel()
                                                            {
                                                                VehicleId = v.VehicleId,
                                                                LicensePlate = v.LicensePlate,
                                                                IsAvailable = v.IsAvailable,
                                                                BrandName = v.Brand.BrandName,
                                                                IsInUse = v.IsInUse,
                                                                BrandId = v.VehicleBrandId,
                                                                VehicleName = v.VehicleName,
                                                                VehiclePayload = v.VehiclePayload
                                                            }).ToList();
        }

        public async Task<Vehicle> GetVehicle(int vehicleId)
        {
            return await _context.Vehicles.Where(v => v.IsDeleted == false)
                                            .Include(v => v.Brand)
                                            .Where(v => v.VehicleId == vehicleId)
                                            .OrderBy(v => v.Brand.BrandName)
                                            .SingleOrDefaultAsync();
        }

        public string IsVehicleInUsedByAnotherDriver(string driverId, int vehicleId, double TStoday)
        {
            var transportsByVehicleToday = _transInfoServices.GetTransportsByVehicleToday(vehicleId, TStoday).ToList();
            if (transportsByVehicleToday != null)
            {
                foreach (var trans in transportsByVehicleToday)
                {
                    if (trans.DayJob.DriverId != driverId && trans.DateCompletedLocal == 0)
                    {
                        return trans.DayJob.DriverId;
                    }
                }
            }
            return String.Empty;
        }
    }
}
