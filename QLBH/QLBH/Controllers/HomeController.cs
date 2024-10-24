using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLBH.Models;
using QLBH.Models.Authentication;
using QLBH.ViewModels;
using System.Diagnostics;
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
        [Authentication("Admin", "Người dùng")]
        public IActionResult Index(int? page, int? maMonAn)
        {
            int pageSize = 8;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstsanpham = db.Monans.AsNoTracking().OrderBy(x => x.TenHh);
            PagedList<Monan> lst = new PagedList<Monan>(lstsanpham, pageNumber, pageSize);

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
    }
}
