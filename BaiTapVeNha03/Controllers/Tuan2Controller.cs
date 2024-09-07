using Microsoft.AspNetCore.Mvc;

namespace BaiTapVeNha03.Controllers
{
    public class Tuan2Controller : Controller
    {
        public IActionResult MayTinh(double a, double b, string pheptinh)
        {
            double ketqua = 0;
            string error = null;

            // Xử lý phép tính dựa trên giá trị của pheptinh
            switch (pheptinh)
            {
                case "cong":
                    ketqua = a + b;
                    break;
                case "tru":
                    ketqua = a - b;
                    break;
                case "nhan":
                    ketqua = a * b;
                    break;
                case "chia":
                    if (b != 0)
                    {
                        ketqua = a / b;
                    }
                    else
                    {
                        error = "Không thể chia cho 0";
                    }
                    break;
                default:
                    error = "Phép tính không hợp lệ";
                    break;
            }

            // Lưu kết quả vào ViewBag
            if (error == null)
            {
                ViewBag.KetQua = ketqua;
            }
            else
            {
                ViewBag.Error = error;
            }
            return View();
        }
    }
}
