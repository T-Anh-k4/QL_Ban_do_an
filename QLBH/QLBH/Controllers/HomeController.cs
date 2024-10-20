using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLBH.Models;
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

        public IActionResult Index(int? page)
        {
            // phân trang
            int pageSize = 8;
            int pageNumber = page ==null||page<0?1:page.Value;
            var lstsanpham = db.Monans.AsNoTracking().OrderBy(x=>x.TenHh);
            PagedList<Monan> lst =new PagedList<Monan>(lstsanpham,pageNumber,pageSize);

            return View(lst);
        }

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
