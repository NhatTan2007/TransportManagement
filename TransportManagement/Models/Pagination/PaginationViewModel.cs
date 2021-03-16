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

        public Pager Pager { get => _pager; set => _pager = value; }
        public IEnumerable<T> Items { get => _items; set => _items = value; }
        public int[] PageSizeItem { get => _pageSizeItem; set => _pageSizeItem = value; }
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
