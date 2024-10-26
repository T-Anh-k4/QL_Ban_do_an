namespace QLBH.Models.ProductModels
{
    public class CartItem
    {
        public int Mahh { get; set; }
        public string TenHH {  get; set; }
        public decimal dongia {  get; set; }
        public int SoLuong {  get; set; }
        public decimal ThanhTien => SoLuong * dongia;

    }
}
