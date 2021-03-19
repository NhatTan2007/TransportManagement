using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransportManagement.Entities;
using TransportManagement.Models.TransportInformation;

namespace TransportManagement.Services.IServices
{
    public interface ITransInfoServices
    {
        Task<bool> CreateNewTransInfo(TransportInformation newTransInfo);
        Task<bool> EditTransInfo(EditTransInfoViewModel transEdit);
        Task<bool> DeleteTransInfo(TransportInformation transDel);
        TransportInformation GetTransport(string transportId);
        ICollection<TransportInformation> GetTransportsToday(double todayTS);
        ICollection<TransportInformation> GetTransportsByVehicleToday(int vehicleId, double todayTimeStamp);
        public ICollection<TransInfoViewModel> GetTransportsToday(double todayTS, int page, int pageSize, string search);
        public ICollection<TransInfoViewModel> GetTransportsToday(double todayTS, int page, int pageSize);
        public ICollection<TransInfoViewModel> GetTransportsThisMonth(double monthTS, int page, int pageSize, string search);
        public ICollection<TransInfoViewModel> GetTransportsThisMonth(double monthTS, int page, int pageSize);
    }
}
