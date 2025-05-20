using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Quanlykytucxa.Models;
using System.Linq;

namespace Quanlykytucxa.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    [Area("Admin")]
    public class StudentsListController : Controller
    {
        private readonly QuanLyKTXContext _context;

        public StudentsListController(QuanLyKTXContext context)
        {
            _context = context;
        }

        public IActionResult Index(string searchString)
        {
            var students = _context.Users.OfType<SinhVien>().AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                students = students.Where(s =>
                    s.TenSv.Contains(searchString) ||
                    s.UserName.Contains(searchString) ||
                    s.Cccd.Contains(searchString) ||
                    s.Sdt.Contains(searchString));
            }

            ViewBag.CurrentFilter = searchString;

            return View(students.ToList());
        }

        public IActionResult Detail(string id)
        {
            var sinhVien = _context.Users.OfType<SinhVien>().FirstOrDefault(s => s.Id == id);
            if (sinhVien == null)
            {
                return NotFound();
            }
            return View(sinhVien);
        }

        public IActionResult UpdateSV(string id)
        {
            var sinhVien = _context.Users.OfType<SinhVien>().FirstOrDefault(s => s.Id == id);
            if (sinhVien == null)
            {
                return NotFound();
            }
            return View(sinhVien);
        }

        [HttpPost]
        public IActionResult UpdateSV(SinhVien model)
        {
            var sv = _context.Users.OfType<SinhVien>().FirstOrDefault(s => s.Id == model.Id);
            if (sv == null) return NotFound();

            // Cập nhật dữ liệu
            sv.TenSv = model.TenSv;
            sv.GioiTinh = model.GioiTinh;
            sv.Sdt = model.Sdt;
            sv.Cccd = model.Cccd;
            sv.NgaySinh = model.NgaySinh;
            sv.DiaChi = model.DiaChi;
            sv.Email = model.Email;
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(string id)
        {
            var sv = _context.Users.OfType<SinhVien>().FirstOrDefault(s => s.Id == id);
            if (sv == null)
            {
                return NotFound();
            }

            _context.Users.Remove(sv); // Xóa sinh viên khỏi DbSet Users
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
