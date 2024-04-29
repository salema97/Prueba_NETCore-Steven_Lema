namespace Shop.Core.Entities.Orders
{
    public class ProductItemOrderd
    {
        public ProductItemOrderd() { }
        public ProductItemOrderd(int productItemId, string productItemName, string pictureUrl)
        {
            ProductItemId = productItemId;
            ProductItemName = productItemName;
            PictureUrl = pictureUrl;
        }

        public int ProductItemId { get; set; }
        public string ProductItemName { get; set; } = string.Empty;
        public string PictureUrl { get; set; } = string.Empty;
    }
}