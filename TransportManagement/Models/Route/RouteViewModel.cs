using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TransportManagement.Models.Route
{
    public class RouteViewModel
    {
        private string _routeId;
        private string _departurePlaceId;
        private string _departurePlace;
        private string _arrivalPlaceId;
        private string _arrivalPlace;
        private int _distance;

        public string RouteId { get => _routeId; set => _routeId = value; }
        public string DeparturePlaceId { get => _departurePlaceId; set => _departurePlaceId = value; }
        public string DeparturePlace { get => _departurePlace; set => _departurePlace = value; }
        public string ArrivalPlaceId { get => _arrivalPlaceId; set => _arrivalPlaceId = value; }
        public string ArrivalPlace { get => _arrivalPlace; set => _arrivalPlace = value; }
        public int Distance { get => _distance; set => _distance = value; }
    }
}
