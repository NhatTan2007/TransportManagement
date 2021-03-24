using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TransportManagement.Models.Driver
{
    public class ReportMonthModel
    {
        private DateTime _month;

        public DateTime Month { get => _month; set => _month = value; }
    }
}
