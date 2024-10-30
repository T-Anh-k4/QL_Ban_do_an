using Microsoft.AspNetCore.Mvc;
using QLBH.Models;
using QLBH.Models.ProductModels;

namespace QLBH.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsAPIController : ControllerBase
    {
        QlbandoanContext db = new QlbandoanContext();

        [HttpGet]
        public IEnumerable<Product> GetAllProducts()
        {
            var SanPham = (from p in db.Monans
                           select new Product
                           {
                               MaMonAn = p.MaMonAn,
                               MaLoai = p.MaLoai,
                               TenHh = p.TenHh,
                               Anh = p.Anh,
                               DonGiaBan = p.DonGiaBan
                           }).ToList();
            return SanPham;
        }

        [HttpGet("{maloai}")]
        public IEnumerable<Product> GetAllProductsByCategory(int maloai)
        {
            var SanPham = (from p in db.Monans
                           where p.MaLoai == maloai
                           select new Product
                           {
                               MaMonAn = p.MaMonAn,
                               MaLoai = p.MaLoai,
                               TenHh = p.TenHh, 
                               Anh = p.Anh,
                               DonGiaBan = p.DonGiaBan
                           }).ToList();
            return SanPham;
        }
    }
}
