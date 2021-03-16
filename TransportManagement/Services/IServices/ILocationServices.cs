using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransportManagement.Entities;
using TransportManagement.Models.Location;

namespace TransportManagement.Services
{
    public interface ILocationServices
    {
        public ICollection<LocationViewModel> GetAllLocations();
        public ICollection<LocationViewModel> GetAllLocations(int page, int pageSize, string search);
        public ICollection<LocationViewModel> GetAllLocations(int page, int pageSize);
        public int CountLocations();
        public Task<bool> CreateLocation(Location newLocation);
        public Task<bool> DeleteLocation(Location location);
        public Location GetLocation(string locationId);
    }
}
