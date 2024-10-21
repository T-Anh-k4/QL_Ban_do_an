using Microsoft.AspNetCore.Mvc;
using QLBH.Models;

namespace QLBH.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin")]
    [Route("admin/homeadmin")]
    public class HomeAdminController : Controller
    {
        QlbandoanContext db = new QlbandoanContext();
        [Route("")]
        [Route("danhmucsanpham")]
        public IActionResult DanhMucSanPham()
        {
            var lstsanpham = db.Monans.ToList();
            return View(lstsanpham);
        }
    }
}
//dangnguancut