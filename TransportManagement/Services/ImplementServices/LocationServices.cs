using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransportManagement.DbContexts;
using TransportManagement.Entities;
using TransportManagement.Models.Location;
using TransportManagement.Services.IServices;

namespace TransportManagement.Services.ImplementServices
{
    public class LocationServices : ILocationServices
    {
        private readonly TransportDbContext _context;

        public LocationServices(TransportDbContext context)
        {
            _context = context;
        }
        public int CountLocations()
        {
            return _context.Locations.Count();
        }

        public async Task<bool> CreateLocation(Location newLocation)
        {
            try
            {
                await _context.Locations.AddAsync(newLocation);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> DeleteLocation(Location location)
        {
            try
            {
                _context.Locations.Remove(location);
                var result = await _context.SaveChangesAsync();
                return result > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> EditLocation(EditLocationViewModel model)
        {
            var location = GetLocation(model.LocationId);
            if (location != null)
            {
                try
                {
                    _context.Locations.Attach(location);
                    location.LocationName = model.LocationName;
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

        public ICollection<LocationViewModel> GetAllLocations()
        {
            return _context.Locations.Select(l => new LocationViewModel
                                                        {
                                                            LocationId = l.LocationId,
                                                            LocationName = l.LocationName
                                                        }).ToList();
        }

        public ICollection<LocationViewModel> GetAllLocations(int page, int pageSize, string search)
        {
            return _context.Locations.Where(l => l.LocationName.Contains(search))
                                        .Skip((page - 1) * pageSize)
                                        .Take(pageSize)
                                        .OrderBy(l => l.LocationName)
                                        .Select(l => new LocationViewModel 
                                        {
                                            LocationId = l.LocationId,
                                            LocationName = l.LocationName
                                        }).ToList();
        }

        public ICollection<LocationViewModel> GetAllLocations(int page, int pageSize)
        {
            return _context.Locations.Skip((page - 1) * pageSize)
                                        .Take(pageSize)
                                        .OrderBy(l => l.LocationName)
                                        .Select(l => new LocationViewModel
                                        {
                                            LocationId = l.LocationId,
                                            LocationName = l.LocationName
                                        }).ToList();
        }

        public Location GetLocation(string locationId)
        {
            return _context.Locations.Where(l => l.LocationId == locationId).SingleOrDefault();
        }
    }
}
