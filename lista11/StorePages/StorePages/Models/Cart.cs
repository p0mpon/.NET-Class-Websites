namespace StorePages.Models
{
    public class CartItem
    {
        public int ArticleId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal TotalValue => Price * Quantity;
    }

    public class CartViewModel
    {
        public List<CartItem> Items { get; set; } = new List<CartItem>();
        public decimal GrandTotal => Items.Sum(i => i.TotalValue);
    }
}