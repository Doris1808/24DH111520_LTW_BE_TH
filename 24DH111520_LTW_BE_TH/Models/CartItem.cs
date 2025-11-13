namespace _24DH111520_LTW_BE_TH.Models
{
    public class CartItem
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;  
        public decimal ProductPrice { get; set; }
        public string? ProductImage { get; set; } 
        public int Quantity { get; set; }
        public decimal Total => ProductPrice * Quantity;
    }
}
