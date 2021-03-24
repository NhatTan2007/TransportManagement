using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TransportManagement.Models.Pagination
{
    public class PaginationViewModel<T> where T : class
    {
        private Pager _pager;
        private IEnumerable<T> _items;
        private int[] _pageSizeItem;
        private string _search;
        private DateTime _startDate;
        private DateTime _endDate;

        public Pager Pager { get => _pager; set => _pager = value; }
        public IEnumerable<T> Items { get => _items; set => _items = value; }
        public int[] PageSizeItem { get => _pageSizeItem; set => _pageSizeItem = value; }
        public string Search { get => _search; set => _search = value; }
        public DateTime StartDate { get => _startDate; set => _startDate = value; }
        public DateTime EndDate { get => _endDate; set => _endDate = value; }

        public PaginationViewModel()
        {
            _pageSizeItem = new int[] { 5, 10, 15 };
        }
        public PaginationViewModel(IEnumerable<T> items)
        {
            _items = items;
            _pageSizeItem = new int[] { 5, 10, 15 };
        }
    }
}
