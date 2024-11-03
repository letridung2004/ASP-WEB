
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project1.Data;
using Project1.Models;
using System.Security.Claims;



namespace Project1.Controllers
{
    [Area("Customer")]
    public class GioHangController : Controller
    {
        private readonly ApplicationDbContext _db;
        public GioHangController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            // Lay thong tin dang nhap
            var identity = (ClaimsIdentity)User.Identity;
            var claim = identity.FindFirst(ClaimTypes.NameIdentifier);

            // Lay danh sach san pham trong gio hang cua user
            GioHangViewModel giohang = new GioHangViewModel
            {

                DsGioHang = _db.GioHang
                                                .Include("SanPham")
                                                .Where(gh => gh.ApplicationUserId == claim.Value)
                                                .ToList(),
                HoaDon = new HoaDon()
            };

            foreach(var item in giohang.DsGioHang)
            {
                // Tinh tien theo so luong
                item.ProductPrice = item.Quantity * item.SanPham.price;
                // TInh tong so trong gio hang
                giohang.HoaDon.Total += item.ProductPrice;
            }
            
            return View(giohang);
        }       

        public IActionResult Xoa(int giohangId)
        {
            var giohang = _db.GioHang.FirstOrDefault(gh => gh.Id == giohangId);

            _db.GioHang.Remove(giohang);

            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Tang(int giohangId)
        {
            var giohang = _db.GioHang.FirstOrDefault(gh => gh.Id == giohangId);

            giohang.Quantity++;

            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Giam(int giohangId)
        {
            var giohang = _db.GioHang.FirstOrDefault(gh => gh.Id == giohangId);

            giohang.Quantity--;

            if (giohang.Quantity == 0)
            {
                _db.GioHang.Remove(giohang);
            }

            _db.SaveChanges();

            return RedirectToAction("Index");
        }
        public IActionResult ThanhToan()
        {
            // Lay thong tin dang nhap
            var identity = (ClaimsIdentity)User.Identity;
            var claim = identity.FindFirst(ClaimTypes.NameIdentifier);

            // Lay danh sach san pham trong gio hang cua user
            GioHangViewModel giohang = new GioHangViewModel
            {

                DsGioHang = _db.GioHang
                  .Include("SanPham")
                  .Where(gh => gh.ApplicationUserId == claim.Value)
                  .ToList(),
                HoaDon = new HoaDon()
            };
            giohang.HoaDon.ApplicationUser = _db.ApplicationUsers.FirstOrDefault(user => user.Id == claim.Value);

            giohang.HoaDon.Name = giohang.HoaDon.ApplicationUser.Name;
            giohang.HoaDon.Address = giohang.HoaDon.ApplicationUser.Address;
            giohang.HoaDon.PhoneNumber = giohang.HoaDon.ApplicationUser.PhoneNumber;

            foreach (var item in giohang.DsGioHang)
            {
                // Tinh tien theo so luong
                item.ProductPrice = item.Quantity * item.SanPham.price;
                // TInh tong so trong gio hang
                giohang.HoaDon.Total += item.ProductPrice;
            }

            return View(giohang);
        }
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult ThanhToan(GioHangViewModel giohang)
        {
            // Lay thong tin tai khoan
            var identity = (ClaimsIdentity)User.Identity;
            var claim = identity.FindFirst(ClaimTypes.NameIdentifier);


            giohang.DsGioHang = _db.GioHang.Include("SanPham").Where(gh => gh.ApplicationUserId == claim.Value).ToList();

            giohang.HoaDon.ApplicationUserId = claim.Value;
            giohang.HoaDon.OrderDate = DateTime.Now;
            giohang.HoaDon.OrderStatus = "Đang xác nhận";

            foreach (var item in giohang.DsGioHang)
            {
                // Tính tien san pham theo so luong
                double prodcutprice = item.Quantity * item.SanPham.price;
                giohang.HoaDon.Total += prodcutprice;
            }

            _db.HoaDon.Add(giohang.HoaDon);
            _db.SaveChanges();

            // Them thong tin chi tiet hoa don
            foreach (var item in giohang.DsGioHang)
            {
                ChiTietHoaDon chitiethoadon = new ChiTietHoaDon()
                {
                    SanPhamId = item.SanPhamId,
                    HoaDonId = giohang.HoaDon.Id,
                    ProductPrice = item.ProductPrice,
                    Quantity = item.Quantity
                };
                _db.ChiTietHoaDon.Add(chitiethoadon);
                _db.SaveChanges();

            }

            // Xoa thong tin trong gio hang
            _db.GioHang.RemoveRange(giohang.DsGioHang);
            _db.SaveChanges();
            // Quay tro ve trang chu
            return RedirectToAction("Index", "Home");
        }
    }
}
