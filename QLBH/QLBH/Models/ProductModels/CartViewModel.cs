namespace QLBH.Models.ProductModels
{
    public class CartViewModel
    {
        public IEnumerable<CartItem> Items { get; set; }
        public decimal TongTien { get; set; }
    }
}
