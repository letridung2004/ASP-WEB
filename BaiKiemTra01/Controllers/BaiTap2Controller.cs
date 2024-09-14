using BaiKiemTra01.Models;
using Microsoft.AspNetCore.Mvc;

namespace BaiKiemTra01.Controllers
{
    public class BaiTap2Controller : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult BaiTap2(SanPhamViewModel SanPham)
        {
            var sanpham = new SanPhamViewModel()
            {
                TenSP = "LapTop",
                GiaBan = 15000000,
                AnhMoTa = "/wwwroot/images/laptop.jpg"
            };
            return View(sanpham);
        }
    }
}
