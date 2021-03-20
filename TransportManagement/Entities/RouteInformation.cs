using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TransportManagement.Entities
{
    public class RouteInformation
    {
        private string _routeId;
        private string _departurePlace;
        private string _arrivalPlace;
        private int _distance;
        [Key]
        public string RouteId { get => _routeId; set => _routeId = value; }
        [Required]
        [MaxLength(200)]
        public string DeparturePlace { get => _departurePlace; set => _departurePlace = value; }
        [Required]
        [MaxLength(200)]
        public string ArrivalPlace { get => _arrivalPlace; set => _arrivalPlace = value; }
        [Required]
        [Range(0, Int16.MaxValue)]
        public int Distance { get => _distance; set => _distance = value; }

        //Foreign Key area
        public string DeparturePlaceId { get; set; }
        [ForeignKey("DeparturePlaceId")]
        public Location Departure { get; set; }
        public string ArrivalPlaceId { get; set; }
        [ForeignKey("ArrivalPlaceId")]
        public Location Arrival { get; set; }
        public ICollection<TransportInformation> Transports { get; set; }
    }
}
