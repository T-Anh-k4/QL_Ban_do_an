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
            }
            return View(); 
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("TaiKhoan");
            return RedirectToAction("Login", "Access");
        }
    }
}
