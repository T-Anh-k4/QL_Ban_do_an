using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLBH.Models;
using QLBH.Models.Authentication;
using QLBH.Models.ProductModels;
using QLBH.ViewModels;
using System.Diagnostics;
using QLBH.Helper;
using X.PagedList;

namespace QLBH.Controllers
{
    public class HomeController : Controller
    {
        QlbandoanContext db = new QlbandoanContext();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
       // [Authentication("Admin", "Người dùng")]
        public IActionResult Index(int? page, int? maMonAn)
        {
            int pageSize = 8;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstsanpham = db.Monans.AsNoTracking().OrderBy(x => x.TenHh);
            PagedList<Monan> lst = new PagedList<Monan>(lstsanpham, pageNumber, pageSize);

            if (TempData["CurrentPage"] != null)
            {
                page = (int)TempData["CurrentPage"];
            }
            // L?y chi ti?t món ?n n?u có mã món ?n
            HomeProductDetailViewModel homeProductDetailViewModel = null;
            if (maMonAn.HasValue)
            {
                var monAn = db.Monans.SingleOrDefault(x => x.MaMonAn == maMonAn.Value);
                var chiTietmonAn = db.Chitietmonans.SingleOrDefault(x => x.MaMonAn == maMonAn.Value);
                homeProductDetailViewModel = new HomeProductDetailViewModel
                {
                    monan = monAn,
                    chitietmonan = chiTietmonAn
                };
            }

            // Tr? v? view kèm c? danh sách và chi ti?t (n?u có)
            ViewBag.CurrentPage = page ?? 1; // Giá tr? trang hi?n t?i
            ViewBag.DetailProduct = homeProductDetailViewModel;
            return View(lst);
        }

        public IActionResult GetTotalPrice(int productId, int quantity)
        {
            var product = db.Monans.FirstOrDefault(m => m.MaMonAn == productId);

            if (product != null)
            {
                // Đảm bảo totalPrice là decimal
                var totalPrice = (decimal)product.DonGiaBan * quantity;
                return Json(new { totalPrice = totalPrice.ToString("N0")});
            }

            return Json(new { totalPrice = "0" });
        }


        public IActionResult SanPhamTheoLoai(int maloai, int? page)
        {
            int pageSize = 8;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstsanpham = db.Monans.AsNoTracking().Where(x => x.MaLoai == maloai).OrderBy(x => x.TenHh);
            PagedList<Monan> lst = new PagedList<Monan>(lstsanpham, pageNumber, pageSize);
            ViewBag.maloai = maloai;
            return View(lst);
        }
        //public IActionResult ChiTietMonAn(int maMonAn)
        //{
        //    var monAn = db.Monans.SingleOrDefault(x => x.MaMonAn == maMonAn);
        //    var chiTietmonAn = db.Chitietmonans.SingleOrDefault(x => x.MaMonAn == maMonAn);
        //    var homeProductDetailViewModel = new HomeProductDetailViewModel 
        //    { 
        //        monan = monAn,
        //        chitietmonan = chiTietmonAn
        //    };
        //    return View(homeProductDetailViewModel);
        //}
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        const string CART_KEY = "MYCART";
        public List<CartItem> cart => HttpContext.Session.Get<List<CartItem>>(CART_KEY) ?? new List<CartItem>();

        [Route("GioHang")]
        public IActionResult GioHang()
        {
            return View(cart);
        }

        public IActionResult ThemGioHang(int id, int quantity = 1)
        {
            var giohang = cart;
            var item = giohang.SingleOrDefault(p => p.Mahh == id);
            if (item == null)
            {
                var hanghoa = db.Monans.SingleOrDefault(p => p.MaMonAn == id);
                if(hanghoa == null)
                {
                    TempData["Message"] = $"Không tìm thấy món ăn có mã {id}";
                    return Redirect("/404");
                }
                item = new CartItem
                {
                    Mahh = hanghoa.MaMonAn,
                    TenHH = hanghoa.TenHh,
                    dongia = hanghoa.DonGiaBan ?? 0,
                    SoLuong = quantity

                };
                giohang.Add(item);
            }
            else
            {
                item.SoLuong += quantity;
            }
            HttpContext.Session.Set(CART_KEY, giohang);
            return RedirectToAction("GioHang");
        }
        [HttpPost]
        [Route("UpdateQuantity")]
        public IActionResult UpdateQuantity(int id, int quantity)
        {
            var giohang = HttpContext.Session.Get<List<CartItem>>(CART_KEY); // Lấy giỏ hàng từ session
            var item = giohang.SingleOrDefault(p => p.Mahh == id); // Tìm sản phẩm theo mã

            if (item != null)
            {
                item.SoLuong = quantity; // Cập nhật số lượng
                HttpContext.Session.Set(CART_KEY, giohang); // Lưu lại giỏ hàng đã cập nhật
            }

            // Có thể trả về kết quả thành công
            return Json(new { success = true });
        }


        [Route("ThanhToan")]
        public IActionResult ThanhToan()
        {
            return View();
       
        }
        [HttpPost]
        public IActionResult Thoat()
        {
            TempData["CurrentPage"] = ViewBag.CurrentPage;
            return RedirectToAction("Index", "Home");
        }
    }
}
