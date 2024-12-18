﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLBH.Models;
using QLBH.Models.Authentication;
using QLBH.Models.ProductModels;
using QLBH.ViewModels;
using System.Diagnostics;
using QLBH.Helper;
using X.PagedList;
using Azure;

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
            ViewBag.CurrentPage = page ?? 1; 
            ViewBag.DetailProduct = homeProductDetailViewModel;
            return View(lst);
        }

        public IActionResult GetTotalPrice(int productId, int quantity)
        {
            var product = db.Monans.FirstOrDefault(m => m.MaMonAn == productId);

            if (product != null)
            {
                var totalPrice = product.DonGiaBan.Value * quantity;
                return Json(new { totalPrice = totalPrice.ToString("N0") + " đ"});
            }
            return Json(new { totalPrice = "0" });
        }
        public IActionResult ChiTietMonAn(int maMonAn)
        {
            var monAn = db.Monans.SingleOrDefault(x => x.MaMonAn == maMonAn);
            var chiTietmonAn = db.Chitietmonans.SingleOrDefault(x => x.MaMonAn == maMonAn);

            if (monAn == null || chiTietmonAn == null)
            {
                return NotFound("Không tìm thấy món ăn này.");
            }
            var viewModel = new HomeProductDetailViewModel
            {
                monan = monAn,
                chitietmonan = chiTietmonAn
            };
            ViewBag.MaMonAn = maMonAn; 
            return View(viewModel);
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
        public async Task<IActionResult> Remove(int id)
        {
            var cart = HttpContext.Session.Get<List<CartItem>>(CART_KEY); 
            cart.RemoveAll(p => p.Mahh == id);
            if(cart.Count == 0)
            {
                HttpContext.Session.Remove(CART_KEY);
            }
            else
            {
                HttpContext.Session.Set(CART_KEY, cart);
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Clear(int id)
        {
            HttpContext.Session.Remove(CART_KEY);
            return RedirectToAction("Index");
        }
        [HttpPost]
        [Route("UpdateQuantity")]
        public IActionResult UpdateQuantity(int id, int quantity=1)
        {
            var giohang = HttpContext.Session.Get<List<CartItem>>(CART_KEY); 
            var item = giohang.SingleOrDefault(p => p.Mahh == id);

            if (item != null)
            {
                item.SoLuong = quantity; 
                HttpContext.Session.Set(CART_KEY, giohang); 
            }

            return Json(new { success = true });
        }


        [Route("ThanhToan")]
        public IActionResult ThanhToan()
        {
            var cart = HttpContext.Session.Get<List<CartItem>>(CART_KEY) ?? new List<CartItem>();
            decimal totalAmount = cart.Sum(item => item.dongia * item.SoLuong);
            ViewBag.TotalAmount = totalAmount;
            return View(cart);
       
        }
        [HttpPost]
        public IActionResult Thoat()
        {
            TempData["CurrentPage"] = ViewBag.CurrentPage;
            return RedirectToAction("Index", "Home");
        }
        public IActionResult GetCartCount()
        {
            var cart = HttpContext.Session.Get<List<CartItem>>(CART_KEY) ?? new List<CartItem>();
            int cartCount = cart.Count;
            HttpContext.Session.SetInt32("CartCount", cartCount);
            return Json(new { count = cartCount });
        }

    }
}
