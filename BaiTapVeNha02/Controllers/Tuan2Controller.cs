using Microsoft.AspNetCore.Mvc;

namespace BaiTapVeNha02.Controllers
{
    public class Tuan2Controller : Controller
    {
        public IActionResult Index()
        {
            ViewBag.HoTen = "Le Tri Dung";
            ViewBag.MSSV = "1822041040";
            ViewBag.Nam = 2024;

            return View();
        }
    }
}
