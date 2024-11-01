using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QLBH.Models;
using QLBH.Models.Authentication;
using X.PagedList;
using QLBH.Models.Dashboard;

namespace QLBH.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin")]
    [Route("admin/homeadmin")]
    [Authentication("Admin")]
    public class HomeAdminController : Controller
    {
        private readonly QlbandoanContext db;

        public HomeAdminController(QlbandoanContext context)
        {
            db = context;
        }

        [Route("")]
        [Route("Index")]
        public IActionResult Index()
        {
            var userCount = db.Nguoidungs.Count();
            var productCount = db.Monans.Count();
            var orderCount = db.Hoadonbans.Count();

            var model = new AdminDashboardViewModel
            {
                UserCount = userCount,
                ProductCount = productCount,
                OrderCount = orderCount
            };

            return View(model); // Truyền model vào Vie
        }

        [Route("")]
        [Route("danhmucsanpham")]
        public IActionResult DanhMucSanPham(int? page)
        {
            int pageSize = 10;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstsanpham = db.Monans.AsNoTracking().OrderBy(x => x.MaMonAn);
            PagedList<Monan> lst = new PagedList<Monan>(lstsanpham, pageNumber, pageSize);

            return View(lst);
        }

        [Route("ThemSanPhamMoi")]
        [HttpGet]
        public IActionResult ThemSanPhamMoi()
        {
            ViewBag.MaLoai = new SelectList(db.Loaimonans.ToList(), "MaLoai", "TenLoai");
            return View();
        }

        [Route("ThemSanPhamMoi")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemSanPhamMoi(Monan SanPham)
        {
            if (ModelState.IsValid)
            {
                db.Monans.Add(SanPham);
                db.SaveChanges();
                return RedirectToAction("DanhMucSanPham");
            }

            // Nếu ModelState không hợp lệ, tải lại danh sách loại món ăn
            ViewBag.MaLoai = new SelectList(db.Loaimonans.ToList(), "MaLoai", "TenLoai");
            return View(SanPham);
        }

        [Route("SuaSanPham")]
        [HttpGet]
        public IActionResult SuaSanPham(int masanpham)
        {
            ViewBag.MaLoai = new SelectList(db.Loaimonans.ToList(), "MaLoai", "TenLoai");
            var sanPham = db.Monans.Find(masanpham);
            return View(sanPham);
        }

        [Route("SuaSanPham")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaSanPham(Monan SanPham)
        {
            if (ModelState.IsValid)
            {
                db.Entry(SanPham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DanhMucSanPham", "HomeAdmin");
            }

            // Nếu ModelState không hợp lệ, tải lại danh sách loại món ăn
            ViewBag.MaLoai = new SelectList(db.Loaimonans.ToList(), "MaLoai", "TenLoai");
            return View(SanPham);
        }
        [Route("XoaSanPham")]
        [HttpGet]
        public IActionResult XoaSanPham(int masanpham)
        {
            TempData["Message"] = "";
            var ChiTietSanPhams =db.Chitietmonans.Where(x=>x.MaMonAn ==  masanpham).ToList();
            if (ChiTietSanPhams.Count() > 0)
            {
                TempData["Message"] = "Không xóa sản phẩm này";
                return RedirectToAction("DanhMucSanPham", "HomeAdmin");
            }
            db.Remove(db.Monans.Find(masanpham));
            db.SaveChanges();
            TempData["Message"] = "đã xóa sản phẩm này";
            return RedirectToAction("DanhMucSanPham", "HomeAdmin");
        }

        // loại món ăn
        [Route("")]
        [Route("LoaiSanPham")]
        public IActionResult LoaiSanPham(int? page)
        {
            int pageSize = 10;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstsanpham = db.Loaimonans.AsNoTracking().OrderBy(x => x.MaLoai);
            PagedList<Loaimonan> lst = new PagedList<Loaimonan>(lstsanpham, pageNumber, pageSize);

            return View(lst);
        }
        [Route("ThemLoaiSanPhamMoi")]
        [HttpGet]
        public IActionResult ThemLoaiSanPhamMoi()
        {
            return View();
        }

        [Route("ThemLoaiSanPhamMoi")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemLoaiSanPhamMoi(Loaimonan SanPham)
        {
            if (ModelState.IsValid)
            {
                db.Loaimonans.Add(SanPham);
                db.SaveChanges();
                return RedirectToAction("LoaiSanPham");
            }
            return View(SanPham);
        }

        [Route("SuaLoaiSanPham")]
        [HttpGet]
        public IActionResult SuaLoaiSanPham(int masanpham)
        {
            var sanPham = db.Loaimonans.Find(masanpham);
            return View(sanPham);
        }

        [Route("SuaLoaiSanPham")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaLoaiSanPham(Loaimonan SanPham)
        {
            if (ModelState.IsValid)
            {
                db.Entry(SanPham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("LoaiSanPham", "HomeAdmin");
            }
            return View(SanPham);
        }

        [Route("XoaLoaiSanPham")]
        [HttpGet]
        public IActionResult XoaLoaiSanPham(int masanpham)
        {
            TempData["Message"] = "";
            var ChiTietSanPhams = db.Monans.Where(x => x.MaLoai == masanpham).ToList();
            if (ChiTietSanPhams.Count() > 0)
            {
                TempData["Message"] = "Không xóa loại sản phẩm này";
                return RedirectToAction("LoaiSanPham", "HomeAdmin");
            }
            db.Remove(db.Loaimonans.Find(masanpham));
            db.SaveChanges();
            TempData["Message"] = "đã xóa loại sản phẩm này";
            return RedirectToAction("LoaiSanPham", "HomeAdmin");
        }

         // Khách hàng
        [Route("")]
        [Route("KhachHang")]
        public IActionResult KhachHang(int? page)
        {
            int pageSize = 10;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstKhachHang = db.Khachhangs.AsNoTracking().OrderBy(x => x.MaKh);
            PagedList<Khachhang> lst = new PagedList<Khachhang>(lstKhachHang, pageNumber, pageSize);

            return View(lst);
        }

        [Route("ThemKhachHangMoi")]
        [HttpGet]
        public IActionResult ThemKhachHangMoi()
        {
            return View();
        }

        [Route("ThemKhachHangMoi")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemKhachHangMoi(Khachhang KhachHang)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra xem tên đăng nhập đã tồn tại chưa
                //if (db.Khachhangs.Any(x => x.MaKh == KhachHang.TaiKhoan))
                //{
                //    ModelState.AddModelError("TaiKhoan", "Tên đăng nhập đã tồn tại.");
                //    return View(KhachHang);
                //}

                // Thêm khách hàng mới
                db.Khachhangs.Add(KhachHang);
                db.SaveChanges();
                return RedirectToAction("KhachHang");
            }
            return View(KhachHang);
        }

        [Route("SuaKhachHang")]
        [HttpGet]
        public IActionResult SuaKhachHang(int maKhachHang)
        {
            var khachHang = db.Khachhangs.Find(maKhachHang);
            return View(khachHang);
        }

        [Route("SuaKhachHang")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaKhachHang(Khachhang KhachHang)
        {
            if (ModelState.IsValid)
            {
                //// Kiểm tra xem tên đăng nhập đã tồn tại cho người dùng khác chưa
                //if (db.Ng.Any(x => x.TaiKhoan == KhachHang.TaiKhoan && x.MaNguoiDung != KhachHang.MaNguoiDung))
                //{
                //    ModelState.AddModelError("TaiKhoan", "Tên đăng nhập đã tồn tại.");
                //    return View(KhachHang);
                //}

                // Cập nhật thông tin khách hàng
                db.Entry(KhachHang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("KhachHang", "HomeAdmin");
            }
            return View(KhachHang);
        }

       [Route("XoaKhachHang")]
       [HttpGet]
       public IActionResult XoaKhachHang(int maKhachHang)
       {
           TempData["Message"] = "";
       
           // Sửa lỗi ở đây: Tìm kiếm khách hàng trong bảng Khachhang
           var khachHang = db.Khachhangs.Find(maKhachHang); 
       
           if (khachHang != null) // Kiểm tra xem khách hàng có tồn tại không
           {
               // Kiểm tra xem có hóa đơn nào liên quan đến khách hàng cần xóa hay không
               var HoaDonBans = db.Hoadonbans.Where(x => x.MaKh == maKhachHang).ToList(); 
               if (HoaDonBans.Count() > 0)
               {
                   TempData["Message"] = "Không xóa khách hàng này vì có hóa đơn liên quan";
                   return RedirectToAction("KhachHang", "HomeAdmin");
               }
       
               db.Khachhangs.Remove(khachHang);
               db.SaveChanges();
               TempData["Message"] = "Đã xóa khách hàng này";
           }
           else
           {
               TempData["Message"] = "Không tìm thấy khách hàng cần xóa";
           }
           return RedirectToAction("KhachHang", "HomeAdmin");
       }
       
    }
}
