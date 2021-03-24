using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TransportManagement.Models.Filter
{
    public class FilterTransPortModel
    {
        private int _page;
        private int _pageSize;
        private string _search;
        private DateTime _startDate;
        private DateTime _endDate;

        public int Page { get => _page; set => _page = value; }
        public int PageSize { get => _pageSize; set => _pageSize = value; }
        public string Search { get => _search; set => _search = value; }
        public DateTime StartDate { get => _startDate; set => _startDate = value; }
        public DateTime EndDate { get => _endDate; set => _endDate = value; }
    }
}
