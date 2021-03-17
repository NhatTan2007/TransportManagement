using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TransportManagement.Models.Location
{
    public class EditLocationViewModel
    {
        private string _locationId;
        private string _locationName;

        public string LocationId { get => _locationId; set => _locationId = value; }
        [Required(ErrorMessage = "Tên địa điểm không được bỏ trống")]
        [Display(Name = "Tên địa điểm")]
        public string LocationName { get => _locationName; set => _locationName = value; }
    }
}
