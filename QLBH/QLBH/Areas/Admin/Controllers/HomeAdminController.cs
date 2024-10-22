using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QLBH.Models;
using X.PagedList;

namespace QLBH.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin")]
    [Route("admin/homeadmin")]
    public class HomeAdminController : Controller
    {
        private readonly QlbandoanContext db;

        public HomeAdminController(QlbandoanContext context)
        {
            db = context;
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
    }
}
