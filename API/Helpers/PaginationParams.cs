

namespace API.Helpers
{
    public class PaginationParams
    {
        private const int MaxPageSize = 50; // max page size
        public int PageNumber { get; set; } = 1; // page number
        private int _pageSize = 10; // page size
        public int PageSize // page size
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value; // if value is greater than max page size, set page size to max page size, else set page size to value
        }
        
    }
}