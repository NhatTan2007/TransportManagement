using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransportManagement.Entities;

namespace TransportManagement.Services.IServices
{
    public interface IDayJobServices
    {
        DayJob GetDayJob(string driverId, double date);
        Task<bool> Create(DayJob newDayJob);
    }
}
