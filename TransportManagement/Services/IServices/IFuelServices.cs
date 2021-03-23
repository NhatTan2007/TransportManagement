using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransportManagement.Entities;
using TransportManagement.Models.Fuel;

namespace TransportManagement.Services.IServices
{
    public interface IFuelServices
    {
        ICollection<FuelViewModel> GetFuels();
        Task<bool> Create(CreateFuelViewModel fuel);
        Task<bool> Edit(EditFuelViewModel fuel);
        Fuel GetFuel(int fuelId);
    }
}
