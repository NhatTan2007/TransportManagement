using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TransportManagement.Entities
{
    public class Location
    {
        private string _locationId;
        private string _locationName;
        [Key]
        public string LocationId { get => _locationId; set => _locationId = value; }
        [Required]
        [MaxLength(200)]
        public string LocationName { get => _locationName; set => _locationName = value; }

    }
}
