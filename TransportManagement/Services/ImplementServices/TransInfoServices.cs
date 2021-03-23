using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransportManagement.DbContexts;
using TransportManagement.Entities;
using TransportManagement.Models.TransportInformation;
using TransportManagement.Services.IServices;
using TransportManagement.Utilities;

namespace TransportManagement.Services.ImplementServices
{
    public class TransInfoServices : ITransInfoServices
    {
        private readonly TransportDbContext _context;

        public TransInfoServices(TransportDbContext context,
                                    UserManager<AppIdentityUser> userManager)
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

        public async Task<bool> EditTransInfo(EditTransInfoViewModel transEdit, string userId)
        {
            var transInfo = GetTransport(transEdit.TransportId);
            if (transInfo != null)
            {
                string editContent = String.Empty;
                _context.TransportInformations.Attach(transInfo);
                if (transInfo.AdvanceMoney != transEdit.AdvanceMoney)
                {
                    editContent += $" Sửa tiền tạm ứng từ \"{transInfo.AdvanceMoney}\" thành \"{transEdit.AdvanceMoney}\" |";
                    transInfo.AdvanceMoney = transEdit.AdvanceMoney;
                }
                if (transInfo.CargoTonnage != transEdit.CargoTonnage)
                {
                    editContent += $" Sửa khối lượng hàng hóa từ \"{transInfo.CargoTonnage}\" thành \"{transEdit.CargoTonnage}\" |";
                    transInfo.CargoTonnage = transEdit.CargoTonnage;
                }
                if (transInfo.CargoTypes != transEdit.CargoTypes)
                {
                    editContent += $" Sửa loại hàng hóa từ \"{transInfo.CargoTypes}\" thành \"{transEdit.CargoTypes}\" |";
                    transInfo.CargoTypes = transEdit.CargoTypes;
                }
                if (transInfo.IsCancel != transEdit.IsCancel)
                {
                    editContent += $" Sửa trạng thái hủy từ \"{transInfo.IsCancel}\" thành \"{transEdit.IsCancel}\" |";
                    transInfo.IsCancel = transEdit.IsCancel;
                }
                if (transInfo.Note != transEdit.Note)
                {
                    editContent += $" Sửa ghi chú từ \"{transInfo.Note}\" thành \"{transEdit.Note}\" |";
                    transInfo.Note = transEdit.Note;
                }
                if (transInfo.ReasonCancel != transEdit.ReasonCancel)
                {
                    editContent += $" Sửa lý do hủy từ \"{transInfo.ReasonCancel}\" thành \"{transEdit.ReasonCancel}\" |";
                    transInfo.ReasonCancel = transEdit.ReasonCancel;
                }
                if (transInfo.ReturnOfAdvances != transEdit.ReturnOfAdvances)
                {
                    editContent += $" Sửa tiền hoàn ứng từ \"{transInfo.ReturnOfAdvances}\" thành \"{transEdit.ReturnOfAdvances}\" |";
                    transInfo.ReturnOfAdvances = transEdit.ReturnOfAdvances;
                }
                if (transInfo.DateCompletedUTC != transEdit.DateCompletedUTC)
                {
                    transInfo.DateCompletedUTC = transEdit.DateCompletedUTC;
                }
                if (transInfo.DateCompletedLocal != transEdit.DateCompletedLocal)
                {
                    editContent += $"Kết thúc chuyến vận chuyển";
                    transInfo.DateCompletedLocal = transEdit.DateCompletedLocal;
                }
                try
                {
                    if (!String.IsNullOrEmpty(editContent))
                    {
                        DateTime localTimeUTC7 = SystemUtilites.ConvertToTimeZone(DateTime.UtcNow, "SE Asia Standard Time");
                        EditTransportInformation newEdit = new EditTransportInformation()
                        {
                            DateEditLocal = SystemUtilites.ConvertToTimeStamp(localTimeUTC7),
                            DateEditUTC = SystemUtilites.ConvertToTimeStamp(DateTime.UtcNow),
                            EditContent = editContent,
                            EditId = Guid.NewGuid().ToString(),
                            TimeZone = "SE Asia Standard Time",
                            TransportId = transEdit.TransportId,
                            UserEditId = userId
                        };
                        _context.EditTransportInformations.Add(newEdit);
                    }
                    var result = await _context.SaveChangesAsync();
                    return result > 0;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return false;
        }

        public TransportInformation GetTransport(string transportId)
        {
            return _context.TransportInformations.Where(t => t.TransportId == transportId)
                                                    .Include(t => t.DayJob)
                                                    .Include(t => t.Route)
                                                    .Include(t => t.Vehicle).ThenInclude(v => v.Fuel)
                                                    .SingleOrDefault();
        }

        public ICollection<TransportInformation> GetTransportsToday(double todayTSLocal)
        {
            return _context.TransportInformations.Where(t => t.DateStartLocal >= todayTSLocal)
                                                    .Include(t => t.DayJob)
                                                    .Include(t => t.Route)
                                                    .Include(t => t.Vehicle)
                                                    .OrderByDescending(t => t.DateStartLocal)
                                                    .ToList();
        }

        public ICollection<TransportInformation> GetTransportsByVehicleToday(int vehicleId, double todayTSLocal)
        {
            return _context.TransportInformations.Where(t => t.DateStartLocal >= todayTSLocal && t.VehicleId == vehicleId)
                                                    .Include(t => t.DayJob)
                                                    .Include(t => t.Route)
                                                    .Include(t => t.Vehicle)
                                                    .OrderByDescending(t => t.DateStartLocal)
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
                                        .OrderByDescending(t => t.DateStartLocal)
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
                            .OrderByDescending(t => t.DateStartLocal)
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
                            .OrderByDescending(t => t.DateStartLocal)
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
                .OrderByDescending(t => t.DateStartLocal)
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

        public async Task<bool> DoneTransInfo(TransportInformation trans, string userId)
        {
            _context.TransportInformations.Attach(trans);
            trans.IsCompleted = true;
            DateTime localTimeUTC7 = SystemUtilites.ConvertToTimeZone(DateTime.UtcNow, "SE Asia Standard Time");
            trans.DateCompletedLocal = SystemUtilites.ConvertToTimeStamp(localTimeUTC7);
            trans.DateCompletedUTC = SystemUtilites.ConvertToTimeStamp(DateTime.UtcNow);
            try
            {
                EditTransportInformation newEdit = new EditTransportInformation()
                {
                    DateEditLocal = SystemUtilites.ConvertToTimeStamp(localTimeUTC7),
                    DateEditUTC = SystemUtilites.ConvertToTimeStamp(DateTime.UtcNow),
                    EditContent = "Kết thúc chuyến vận chuyển",
                    EditId = Guid.NewGuid().ToString(),
                    TimeZone = "SE Asia Standard Time",
                    TransportId = trans.TransportId,
                    UserEditId = userId
                };
                _context.EditTransportInformations.Add(newEdit);
                var result = await _context.SaveChangesAsync();
                return result > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
