using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransportManagement.Entities;
using TransportManagement.Models.Route;

namespace TransportManagement.Services.IServices
{
    public interface IRouteServices
    {
        public ICollection<RouteViewModel> GetAllRoutes();
        public ICollection<RouteViewModel> GetAllRoutes(int page, int pageSize, string search);
        public ICollection<RouteViewModel> GetAllRoutes(int page, int pageSize);
        public int CountRoutes();
        public Task<bool> CreateRoute(RouteInformation newRoute);
        public Task<bool> DeleteRoute(RouteInformation route);
        public RouteInformation GetRoute(string routeId);
        public bool IsRouteExists(string departureId, string arrivalId);
        public Task<bool> EditRoute(EditRouteViewModel model);
    }
}
