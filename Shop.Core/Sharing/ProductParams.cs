namespace Shop.Core.Sharing
{
    public class ProductParams
    {
        private int _pageSize = 13;
        public int MaxPageSize { get; set; } = 50;

        private string? _search;
        public int PageNumber { get; set; } = 1;
        public int? CategoryId { get; set; }
        public string? Sorting { get; set; }

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value > MaxPageSize ? MaxPageSize : value;
        }

        public string? Search
        {
            get { return _search; }
            set { _search = value?.ToLower(); }
        }
    }
}
