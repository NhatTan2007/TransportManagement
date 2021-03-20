using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TransportManagement.Entities
{
    public class DayJob
    {
        private string _dayJobId;
        private double _date;
        [Key]
        public string DayJobId { get => _dayJobId; set => _dayJobId = value; }
        [Required]
        public double Date { get => _date; set => _date = value; }

        //Foreign Key area
        [Required]
        public string DriverId { get; set; }
        [ForeignKey("DriverId")]
        public AppIdentityUser Driver { get; set; }
        public ICollection<TransportInformation> Transports { get; set; }
    }
}
