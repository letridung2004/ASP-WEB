using BaiKiemTra01.Models;
using Microsoft.AspNetCore.Mvc;

namespace BaiKiemTra01.Controllers
{
    public class TaiKhoanControllercs : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult DangKy(TaiKhoanViewModel taikhoan)
        {
            if (taikhoan != null && taikhoan.Password != null && taikhoan.Password.Length > 0)
            {
                taikhoan.Password = taikhoan.Password + "_da_ma_hoa";
            }
            return View(taikhoan);
        }
    }
}
