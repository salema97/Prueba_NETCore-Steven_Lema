namespace Shop.Core.Sharing
{
    public class ProductParams
    {
        public int Maxpagesize { get; set; } = 50;
        private int pagesize = 13;

        public int PageNumber { get; set; } = 1;
        public int? CategoryId { get; set; }
        public string Sorting { get; set; }
        private string _search;

        public int Pagesize
        {
            get => pagesize;
            set => pagesize = value > Maxpagesize ? Maxpagesize : value;
        }

        public string Search
        {
            get { return _search; }
            set { _search = value.ToLower(); }
        }
    }
}
