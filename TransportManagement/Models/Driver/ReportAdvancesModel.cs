using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TransportManagement.Models.Driver
{
    public class ReportAdvancesModel
    {
        private decimal _advances;
        private decimal _returnOfAdvances;
        [Display(Name = "Tiền tạm ứng")]
        [Column("decimal(18,0)")]
        public decimal Advances { get => _advances; set => _advances = value; }
        [Display(Name = "Tiền đã hoàn tạm ứng")]
        [Column("decimal(18,0)")]
        public decimal ReturnOfAdvances { get => _returnOfAdvances; set => _returnOfAdvances = value; }
        public decimal AdvancesHaveToReturn => _advances - _returnOfAdvances;
    }
}
