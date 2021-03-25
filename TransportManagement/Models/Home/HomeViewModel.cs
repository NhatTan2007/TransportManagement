using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TransportManagement.Models.Home
{
    public class HomeViewModel
    {
        public int VehiclesCount { get; set; }
        public int VehiclesBusyCount { get; set; }
        public int DriversCount { get; set; }
        public int DriverBusyCount { get; set; }
        public int TransportInMonth { get; set; }
        public int TransportCompeletedInMonth { get; set; }
        [Column(TypeName = "decimal(18,0)")]
        public decimal AdvancesInMonth { get; set; }
    }
}
