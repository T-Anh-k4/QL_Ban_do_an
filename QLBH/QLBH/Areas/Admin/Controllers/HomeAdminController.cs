using Microsoft.AspNetCore.Mvc;
using QLBH.Models;

namespace QLBH.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin")]
    [Route("admin/homeadmin")]
    public class HomeAdminController : Controller
    {
        Monan db = new Monan();
        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
      
            return View();
        }
        public IActionResult DanhMucSanPham()
        {
            return View();
        }
    }
}
