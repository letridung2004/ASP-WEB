using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace BaiKiemTra03_03.Controllers
{
    public class ContractController : Controller
    {
        private readonly AppDbContext _context;

        public ContractController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var contracts = _context.Contracts.Include(c => c.Customer).ToList();
            return View(contracts);
        }

        // Thêm các action: Create, Edit, Delete, Details
    }




}
