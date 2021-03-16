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

        public RouteServices(TransportDbContext context)
        {
            _context = context;
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
    }
}
