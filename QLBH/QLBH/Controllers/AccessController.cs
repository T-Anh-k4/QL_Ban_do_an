using Microsoft.AspNetCore.Mvc;
using QLBH.Models;

namespace QLBH.Controllers
{
    public class AccessController : Controller
    {
        QlbandoanContext db = new QlbandoanContext();
        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("TaiKhoan") == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index","Home");
            }
        }
        [HttpPost]
        public IActionResult Login(Nguoidung user)
        {
            if (HttpContext.Session.GetString("TaiKhoan") == null)
            {
                var u=db.Nguoidungs.Where(x=>x.TaiKhoan.Equals(user.TaiKhoan) && x.MatKhau.Equals(user.MatKhau)).FirstOrDefault();
                if (u != null)
                {
                    HttpContext.Session.SetString("TaiKhoan", u.TaiKhoan.ToString());
                    HttpContext.Session.SetString("Loai", u.Loai.ToString());

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Tài khoản hoặc mật khẩu không đúng.");
            }
            return View(); 
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("TaiKhoan");
            return RedirectToAction("Login", "Access");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [HttpPost]
        public IActionResult Register(Nguoidung newUser)
        {
            if (ModelState.IsValid)
            {
                var existingUser = db.Nguoidungs.FirstOrDefault(x => x.TaiKhoan == newUser.TaiKhoan);
                if (existingUser == null)
                {
                    newUser.Loai = "Người dùng";
                    db.Nguoidungs.Add(newUser);
                    db.SaveChanges();
                    TempData["SuccessMessage"] = "Đăng ký thành công! Bạn có thể đăng nhập ngay bây giờ.";
                    return RedirectToAction("Login", "Access");
                }
                ViewBag.ErrorMessage = "Tài khoản đã tồn tại. Vui lòng chọn tài khoản khác.";
            }
            return View(newUser);
        }

    }
}
