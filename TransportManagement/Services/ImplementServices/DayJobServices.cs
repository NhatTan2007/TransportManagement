using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransportManagement.DbContexts;
using TransportManagement.Entities;
using TransportManagement.Services.IServices;
using TransportManagement.Utilities;

namespace TransportManagement.Services.ImplementServices
{
    public class DayJobServices : IDayJobServices
    {
        private readonly TransportDbContext _context;

        public DayJobServices(TransportDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Create(DayJob newDayJob)
        {
            try
            {
                _context.DayJobs.Add(newDayJob);
                var result = await _context.SaveChangesAsync();
                return result > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public DayJob GetDayJob(string driverId, double date)
        {
            SystemUtilites.GetDateTimeFromTimeStamp(date);
            return _context.DayJobs.Where(j => j.DriverId == driverId && j.Date == date)
                                    .Include(j => j.Driver)
                                    .Include(j => j.Transports).ThenInclude(t => t.Vehicle)
                                    .Include(j => j.Transports).ThenInclude(t => t.Route)
                                    .SingleOrDefault();
        }
    }
}
