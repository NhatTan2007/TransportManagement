using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TransportManagement.Models.Route
{
    public class CreateRouteViewModel
    {
        private string _departurePlaceId;
        private string _arrivalPlaceId;
        private int _distance;
        [Required(ErrorMessage = "Địa điểm xuất phát không được để trống")]
        [Display(Name = "Địa điểm xuất phát")]
        public string DeparturePlaceId { get => _departurePlaceId; set => _departurePlaceId = value; }
        [Required(ErrorMessage = "Địa điểm đến không được để trống")]
        [Display(Name = "Địa điểm đến")]
        public string ArrivalPlaceId { get => _arrivalPlaceId; set => _arrivalPlaceId = value; }
        [Required(ErrorMessage = "Chiều dài tuyến đường không được để trống")]
        [Range(0, Int32.MaxValue, ErrorMessage = "Giá trị không đúng, xin mời nhập lại")]
        [Display(Name = "Chiều dài tuyến đường")]
        public int Distance { get => _distance; set => _distance = value; }
    }
}
