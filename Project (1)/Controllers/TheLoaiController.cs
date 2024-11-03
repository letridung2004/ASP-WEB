using Project1.Data;
using Project1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Linq;

namespace Project1.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class TheLoaiController : Controller
    {
        private readonly ApplicationDbContext _db;

        public TheLoaiController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var theloai = _db.TheLoai.ToList();
            ViewBag.TheLoai = theloai;
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(TheLoai theloai)
        {
            if (ModelState.IsValid)
            {
                _db.TheLoai.Add(theloai);
                _db.SaveChanges();
                TempData["SuccessMessage"] = "Thêm thể loại thành công!";
                return RedirectToAction("Index");
            }
            return View(theloai);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var theloai = _db.TheLoai.Find(id);
            if (theloai == null)
            {
                return NotFound();
            }
            return View(theloai);
        }

        [HttpPost]
        public IActionResult Edit(TheLoai theloai)
        {
            if (ModelState.IsValid)
            {
                _db.TheLoai.Update(theloai);
                _db.SaveChanges();
                TempData["SuccessMessage"] = "Cập nhật thể loại thành công!";
                return RedirectToAction("Index");
            }
            return View(theloai);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var theloai = _db.TheLoai.Find(id);
            if (theloai == null)
            {
                return NotFound();
            }
            return View(theloai);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            var theloai = _db.TheLoai.Find(id);
            if (theloai == null)
            {
                return NotFound();
            }
            _db.TheLoai.Remove(theloai);
            _db.SaveChanges();
            TempData["SuccessMessage"] = "Xóa thể loại thành công!";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var theloai = _db.TheLoai.Find(id);
            if (theloai == null)
            {
                return NotFound();
            }
            return View(theloai);
        }

        [HttpGet]
        public IActionResult Search(string searchString)
        {
            var theloai = string.IsNullOrEmpty(searchString)
                ? _db.TheLoai.ToList()
                : _db.TheLoai.Where(tl => tl.Name.Contains(searchString)).ToList();

            ViewBag.SearchString = searchString;
            ViewBag.TheLoai = theloai;
            return View("Index");
        }
    }
}
