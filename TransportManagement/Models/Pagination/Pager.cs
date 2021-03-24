using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TransportManagement.Models.Pagination
{
    public class Pager
    {
        private int _totalItems;
        private int _currentPage;
        private int _startPage;
        private int _endPage;
        private int _pageSize;
        private int _nextPage;
        private int _previousPage;
        private int _maxShowPage;

        public int TotalItems { get => _totalItems; set => _totalItems = value; }
        public int CurrentPage { get => _currentPage; set => _currentPage = value; }
        public int StartPage { get => _startPage; set => _startPage = value; }
        public int EndPage { get => _endPage; set => _endPage = value; }
        public int TotalPages => (int)Math.Ceiling((double)_totalItems / (double)_pageSize);
        public int PageSize { get => _pageSize; set => _pageSize = value; }
        public int NextPage { get => _nextPage; set => _nextPage = value; }
        public int PreviousPage { get => _previousPage; set => _previousPage = value; }

        public Pager(int totalItems, int currentPage, int pageSize)
        {
            _totalItems = totalItems;
            _pageSize = pageSize;
            _currentPage = currentPage;
            _maxShowPage = 5;  //Must be odd not even numbers and not less than 5
            if (TotalPages > _maxShowPage)
            {
                if (_currentPage < 3)
                {
                    _startPage = 1;
                    _endPage = _maxShowPage;
                }
                if (_currentPage >= 3 && _currentPage <= TotalPages - 2)
                {
                    _startPage = _currentPage - (_maxShowPage - 1) / 2;
                    _endPage = _currentPage + (_maxShowPage - 1) / 2;
                }
                if (_currentPage > TotalPages - (_maxShowPage - 1) / 2 && _currentPage <= TotalPages)
                {
                    _endPage = TotalPages;
                    _startPage = TotalPages - _maxShowPage + 1;
                }
            }
            if (TotalPages <= _maxShowPage)
            {
                _startPage = 1;
                _endPage = TotalPages;
            }
            if (_currentPage > 1)
            {
                _previousPage = _currentPage - 1;
            }
            else
            {
                _previousPage = 1;
            }

            if (_currentPage < TotalPages)
            {
                _nextPage = _currentPage + 1;
            }
            else
            {
                _nextPage = TotalPages;
            }
        }
    }
}
