using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransportManagement.DbContexts;
using TransportManagement.Entities;
using TransportManagement.Models.Fuel;
using TransportManagement.Services.IServices;

namespace TransportManagement.Services.ImplementServices
{
    public class FuelServices : IFuelServices
    {
        private readonly TransportDbContext _context;

        public FuelServices(TransportDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Create(CreateFuelViewModel fuel)
        {
            try
            {
                Fuel newFuel = new Fuel()
                {
                    FuelName = fuel.FuelName,
                    FuelPrice = fuel.FuelPrice
                };
                _context.Add(newFuel);
                var result = await _context.SaveChangesAsync();
                return result > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Edit(EditFuelViewModel fuel)
        {
            Fuel editFuel = GetFuel(fuel.FuelId);
            if (editFuel != null)
            {
                try
                {
                    _context.Fuels.Attach(editFuel);
                    editFuel.FuelName = fuel.FuelName;
                    editFuel.FuelPrice = fuel.FuelPrice;
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

        public Fuel GetFuel(int fuelId)
        {
            return _context.Fuels.Where(f => f.FuelId == fuelId).Select(f => f).SingleOrDefault();
        }

        public ICollection<FuelViewModel> GetFuels()
        {
            return _context.Fuels.Select(f => new FuelViewModel() 
                                                        {
                                                            FuelId = f.FuelId,
                                                            FuelName = f.FuelName,
                                                            FuelPrice = f.FuelPrice
                                                        }).ToList();
        }
    }
}
