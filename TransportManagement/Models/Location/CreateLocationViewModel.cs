using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TransportManagement.Models.Location
{
    public class CreateLocationViewModel
    {
        private string _locationName;
        [Required(ErrorMessage = "Tên địa điểm không được để trống")]
        [MaxLength(ErrorMessage = "Độ dài tối đa cho phép là 200 ký tự")]
        [Display(Name = "Tên địa điểm")]
        public string LocationName { get => _locationName; set => _locationName = value; }
    }
}
