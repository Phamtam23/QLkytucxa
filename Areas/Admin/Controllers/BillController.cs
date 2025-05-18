using Microsoft.AspNetCore.Mvc;
using Quanlykytucxa.Models; // nơi chứa class HoaDon
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;

namespace Quanlykytucxa.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BillController : Controller
    {
        private readonly QuanLyKTXContext _context;

        public BillController(QuanLyKTXContext context)
        {
            _context = context;
        }

        public IActionResult Index(int? month, int? year)
        {
            var selectedMonth = new DateTime(year ?? DateTime.Now.Year, month ?? DateTime.Now.Month, 1);

            var hoaDons = _context.HoaDons
                .Include(h => h.MaDnNavigation)
                    .ThenInclude(dn => dn.MaPhongNavigation)
                .Where(h => h.Ngaytao.HasValue &&
                            h.Ngaytao.Value.Month == selectedMonth.Month &&
                            h.Ngaytao.Value.Year == selectedMonth.Year)
                .ToList();

            ViewBag.SelectedMonth = selectedMonth;
            return View(hoaDons);
        }

        public IActionResult Create(int month, int year)
        {
            var ngayTao = new DateTime(year, month, 1);
            // Truyền view để chọn phòng, nhập số điện nước, etc.
            ViewBag.NgayTao = ngayTao;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(HoaDon hoaDon)
        {
            if (ModelState.IsValid)
            {
                hoaDon.Ngaytao = DateTime.Now;

                _context.Add(hoaDon);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            return View(hoaDon);
        }
    }
}
