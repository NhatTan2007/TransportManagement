using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransportManagement.DbContexts;
using TransportManagement.Entities;
using TransportManagement.Models.Route;
using TransportManagement.Services.IServices;

namespace TransportManagement.Services.ImplementServices
{
    public class RouteServices : IRouteServices
    {
        private readonly TransportDbContext _context;
        private readonly ILocationServices _locationServices;

        public RouteServices(TransportDbContext context,
                                ILocationServices locationServices)
        {
            _context = context;
            _locationServices = locationServices;
        }
        public int CountRoutes()
        {
            return _context.RouteInformations.Count();
        }

        public async Task<bool> CreateRoute(RouteInformation newRoute)
        {
            try
            {
                await _context.RouteInformations.AddAsync(newRoute);
                var result = await _context.SaveChangesAsync();
                return result > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> DeleteRoute(RouteInformation route)
        {
            try
            {
                _context.RouteInformations.Remove(route);
                var result = await _context.SaveChangesAsync();
                return result > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> EditRoute(EditRouteViewModel model)
        {
            var arrivalPlace = _locationServices.GetLocation(model.ArrivalPlaceId);
            var departurePlace = _locationServices.GetLocation(model.DeparturePlaceId);
            var routeEdit = GetRoute(model.RouteId);
            //map new data to route
            if (arrivalPlace != null && departurePlace != null && routeEdit != null)
            {
                try
                {
                    _context.Attach(routeEdit);
                    routeEdit.DeparturePlaceId = departurePlace.LocationId;
                    routeEdit.DeparturePlace = departurePlace.LocationName;
                    routeEdit.ArrivalPlaceId = arrivalPlace.LocationId;
                    routeEdit.ArrivalPlace = arrivalPlace.LocationName;
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

        public ICollection<RouteViewModel> GetAllRoutes()
        {
            return _context.RouteInformations.Select(r => new RouteViewModel
                                                            {
                                                                RouteId = r.RouteId,
                                                                ArrivalPlaceId = r.ArrivalPlaceId,
                                                                ArrivalPlace = r.ArrivalPlace,
                                                                DeparturePlaceId = r.DeparturePlaceId,
                                                                DeparturePlace = r.DeparturePlace,
                                                                Distance = r.Distance
                                                            }).ToList();
        }

        public ICollection<RouteViewModel> GetAllRoutes(int page, int pageSize, string search)
        {
            return _context.RouteInformations.Where(r => r.ArrivalPlace.Contains(search) || r.DeparturePlace.Contains(search))
                            .Skip((page - 1) * pageSize)
                                .Take(pageSize)
                                    .Select(r => new RouteViewModel
                                    {
                                        RouteId = r.RouteId,
                                        ArrivalPlaceId = r.ArrivalPlaceId,
                                        ArrivalPlace = r.ArrivalPlace,
                                        DeparturePlaceId = r.DeparturePlaceId,
                                        DeparturePlace = r.DeparturePlace,
                                        Distance = r.Distance
                                    }).ToList();
        }

        public ICollection<RouteViewModel> GetAllRoutes(int page, int pageSize)
        {
            return _context.RouteInformations.Skip((page - 1) * pageSize)
                                                .Take(pageSize)
                                                    .Select(r => new RouteViewModel
                                                    {
                                                        RouteId = r.RouteId,
                                                        ArrivalPlaceId = r.ArrivalPlaceId,
                                                        ArrivalPlace = r.ArrivalPlace,
                                                        DeparturePlaceId = r.DeparturePlaceId,
                                                        DeparturePlace = r.DeparturePlace,
                                                        Distance = r.Distance
                                                    }).ToList();
        }

        public RouteInformation GetRoute(string routeId)
        {
            return _context.RouteInformations.Where(r => r.RouteId == routeId).SingleOrDefault();
        }

        public bool IsRouteExists(string departureId, string arrivalId)
        {
            var route = _context.RouteInformations
                        .Where(r => r.DeparturePlaceId == departureId && r.ArrivalPlaceId == arrivalId)
                        .SingleOrDefault();
            if (route != null)
            {
                return true;
            }
            return false;
        }
    }
}
