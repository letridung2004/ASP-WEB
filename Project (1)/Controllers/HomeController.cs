using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project1.Data;
using Project1.Models;
using Project1.Data;
using Project1.Models;
using System.Diagnostics;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Project1.Controllers
{
	[Area("Customer")]
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly ApplicationDbContext _db;

		public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
		{
			_logger = logger;
			_db = db;
		}

		public IActionResult Index()
		{
			IEnumerable<SanPham> sanpham = _db.SanPham.Include("TheLoai").ToList();
			return View(sanpham);
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

		[HttpGet]
        public IActionResult Details(int sanphamId)
        {
			GioHang giohang = new GioHang()
			{
				SanPhamId = sanphamId,
				SanPham = _db.SanPham.Include("TheLoai").FirstOrDefault(sp => sp.Id == sanphamId),
				Quantity = 1
			};
            return View(giohang);
        }
		[HttpPost]
		[Authorize]
        public IActionResult Details(GioHang giohang)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var claim = identity.FindFirst(ClaimTypes.NameIdentifier);
			giohang.ApplicationUserId = claim.Value;

			// Kiem tra san pham da co trong gio hang chua
			var giohangdb = _db.GioHang.FirstOrDefault(sp => sp.SanPhamId == giohang.SanPhamId && sp.ApplicationUserId == giohang.ApplicationUserId);
			if (giohangdb == null)
			{
				_db.Add(giohang);
			}
			else
			{
				giohangdb.Quantity += giohang.Quantity;
			}
			// Luu xuong co so du lieu
			_db.SaveChanges();

            return RedirectToAction("Index");
        }
        public IActionResult FilterByTheLoai(int id)
        {
            IEnumerable<SanPham> sanpham = _db.SanPham.Include("TheLoai").Where(sp => sp.TheLoai.Id == id).ToList();
            return View("Index",sanpham);
        }
		
		
			
		
    }
}