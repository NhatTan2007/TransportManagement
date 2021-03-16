using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TransportManagement.Models.Location
{
    public class LocationViewModel
    {
        private string _locationId;
        private string _locationName;

        public string LocationId { get => _locationId; set => _locationId = value; }
        public string LocationName { get => _locationName; set => _locationName = value; }
    }
}
