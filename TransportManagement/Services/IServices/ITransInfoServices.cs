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
        Task<bool> EditTransInfo(EditTransInfoViewModel transEdit, string userId);
        Task<bool> DeleteTransInfo(TransportInformation transDel);
        Task<bool> DoneTransInfo(TransportInformation trans, string userId);
        Task<TransportInformation> GetTransport(string transportId);
        Task<ICollection<TransportInformation>> GetTransportsToday(double todayTS);
        Task<ICollection<TransportInformation>> GetTransportsByVehicleToday(int vehicleId, double todayTimeStamp);
        Task<ICollection<TransportInformation>> GetTransportsNotFinishByVehicle(int vehicleId);
        Task<ICollection<TransportInformation>> GetTransportsNotFinishByDriver(string driverId);
        Task<ICollection<TransInfoViewModel>> GetTransports(double startDate, double endDate, string search);
        Task<ICollection<TransInfoViewModel>> GetTransportsCompeleted(double startDate, double endDate, string search);
        Task<ICollection<TransInfoViewModel>> GetTransportsDoneByDriver(double startDate, double endDate, string driverId);
        Task<ICollection<TransInfoViewModel>> GetTransportsNotDoneByDriver(double startDate, double endDate, string driverId);
        Task<ICollection<EditTransportInformation>> Histories(string transportId);

        
    }
}
