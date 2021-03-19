using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransportManagement.DbContexts;
using TransportManagement.Entities;
using TransportManagement.Models.TransportInformation;
using TransportManagement.Services.IServices;

namespace TransportManagement.Services.ImplementServices
{
    public class TransInfoServices : ITransInfoServices
    {
        private readonly TransportDbContext _context;

        public TransInfoServices(TransportDbContext context)
        {
            _context = context;
        }
        public async Task<bool> CreateNewTransInfo(TransportInformation newTransInfo)
        {
            try
            {
                _context.TransportInformations.Add(newTransInfo);
                var result = await _context.SaveChangesAsync();
                return result > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteTransInfo(TransportInformation transDel)
        {
            try
            {
                _context.TransportInformations.Remove(transDel);
                var result = await _context.SaveChangesAsync();
                return result > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Task<bool> EditTransInfo(EditTransInfoViewModel transEdit)
        {
            throw new NotImplementedException();
        }

        public TransportInformation GetTransport(string transportId)
        {
            return _context.TransportInformations.Where(t => t.TransportId == transportId)
                                                    .Include(t => t.DayJob)
                                                    .Include(t => t.Route)
                                                    .Include(t => t.Vehicle)
                                                    .SingleOrDefault();
        }

        public ICollection<TransportInformation> GetTransportsToday(double todayTSLocal)
        {
            return _context.TransportInformations.Where(t => t.DateStartLocal >= todayTSLocal)
                                                    .Include(t => t.DayJob)
                                                    .Include(t => t.Route)
                                                    .Include(t => t.Vehicle)
                                                    .ToList();
        }

        public ICollection<TransportInformation> GetTransportsByVehicleToday(int vehicleId, double todayTSLocal)
        {
            return _context.TransportInformations.Where(t => t.DateStartLocal >= todayTSLocal && t.VehicleId == vehicleId)
                                                    .Include(t => t.DayJob)
                                                    .Include(t => t.Route)
                                                    .Include(t => t.Vehicle)
                                                    .ToList();
        }

        public ICollection<TransInfoViewModel> GetTransportsToday(double todayTS, int page, int pageSize, string search)
        {
            return _context.TransportInformations.Where(t => t.DateStartLocal >= todayTS)
                                        .Include(t => t.DayJob).ThenInclude(j => j.Driver)
                                        .Include(t => t.Route)
                                        .Include(t => t.Vehicle)
                                        .Where(t => t.DayJob.Driver.FullName.Contains(search)
                                                    || t.Vehicle.LicensePlate.Contains(search))
                                        .Select(t => new TransInfoViewModel
                                        {
                                            AdvanceMoney = t.AdvanceMoney,
                                            CargoTonnage = t.CargoTonnage,
                                            DateStartLocal = t.DateStartLocal,
                                            DriverName = t.DayJob.Driver.FullName,
                                            IsCancel = t.IsCancel,
                                            IsCompleted = t.IsCompleted,
                                            ReturnOfAdvances = t.ReturnOfAdvances,
                                            TransportId = t.TransportId,
                                            VehicleLicensePlate = t.Vehicle.LicensePlate
                                        })
                                        .ToList();
        }

        public ICollection<TransInfoViewModel> GetTransportsToday(double todayTS, int page, int pageSize)
        {
            return _context.TransportInformations.Where(t => t.DateStartLocal >= todayTS)
                            .Include(t => t.DayJob).ThenInclude(j => j.Driver)
                            .Include(t => t.Route)
                            .Include(t => t.Vehicle)
                            .Select(t => new TransInfoViewModel
                            {
                                AdvanceMoney = t.AdvanceMoney,
                                CargoTonnage = t.CargoTonnage,
                                DateStartLocal = t.DateStartLocal,
                                DriverName = t.DayJob.Driver.FullName,
                                IsCancel = t.IsCancel,
                                IsCompleted = t.IsCompleted,
                                ReturnOfAdvances = t.ReturnOfAdvances,
                                TransportId = t.TransportId,
                                VehicleLicensePlate = t.Vehicle.LicensePlate
                            })
                            .ToList();
        }

        public ICollection<TransInfoViewModel> GetTransportsThisMonth(double monthTS, int page, int pageSize, string search)
        {
            return _context.TransportInformations.Where(t => t.DateStartLocal >= monthTS)
                            .Include(t => t.DayJob).ThenInclude(j => j.Driver)
                            .Include(t => t.Route)
                            .Include(t => t.Vehicle)
                            .Where(t => t.DayJob.Driver.FullName.Contains(search)
                                        || t.Vehicle.LicensePlate.Contains(search))
                            .Select(t => new TransInfoViewModel
                            {
                                AdvanceMoney = t.AdvanceMoney,
                                CargoTonnage = t.CargoTonnage,
                                DateStartLocal = t.DateStartLocal,
                                DriverName = t.DayJob.Driver.FullName,
                                IsCancel = t.IsCancel,
                                IsCompleted = t.IsCompleted,
                                ReturnOfAdvances = t.ReturnOfAdvances,
                                TransportId = t.TransportId,
                                VehicleLicensePlate = t.Vehicle.LicensePlate
                            })
                            .ToList();
        }

        public ICollection<TransInfoViewModel> GetTransportsThisMonth(double monthTS, int page, int pageSize)
        {
            return _context.TransportInformations.Where(t => t.DateStartLocal >= monthTS)
                .Include(t => t.DayJob).ThenInclude(j => j.Driver)
                .Include(t => t.Route)
                .Include(t => t.Vehicle)
                .Select(t => new TransInfoViewModel
                {
                    AdvanceMoney = t.AdvanceMoney,
                    CargoTonnage = t.CargoTonnage,
                    DateStartLocal = t.DateStartLocal,
                    DriverName = t.DayJob.Driver.FullName,
                    IsCancel = t.IsCancel,
                    IsCompleted = t.IsCompleted,
                    ReturnOfAdvances = t.ReturnOfAdvances,
                    TransportId = t.TransportId,
                    VehicleLicensePlate = t.Vehicle.LicensePlate
                })
                .ToList();
        }
    }
}
