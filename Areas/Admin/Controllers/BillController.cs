using Microsoft.AspNetCore.Mvc;
using Quanlykytucxa.Models; // nơi chứa class HoaDon
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Quanlykytucxa.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    [Area("Admin")]
    public class BillController : Controller
    {
        private readonly QuanLyKTXContext _context;

        public BillController(QuanLyKTXContext context)
        {
            _context = context;
        }

        private bool HoaDonExists(string id)
        {
            return _context.HoaDons.Any(e => e.MaHoaDon == id);
        }

        public IActionResult Index(int? month, int? year)
        {
            var selectedMonth = new DateTime(
                year ?? DateTime.Now.Year,
                month ?? DateTime.Now.Month,
                1
            );

            // Tìm những hóa đơn có ngày lập = selectedMonth + 1 tháng
            var hoaDons = _context.HoaDons
                .Include(h => h.MaDnNavigation)
                    .ThenInclude(dn => dn.MaPhongNavigation)
                .Where(h => h.Ngaytao.HasValue &&
                            h.Ngaytao.Value.Month == selectedMonth.AddMonths(1).Month &&
                            h.Ngaytao.Value.Year == selectedMonth.AddMonths(1).Year)
                .ToList();

            ViewBag.SelectedMonth = selectedMonth;
            return View(hoaDons);
        }

        public IActionResult Create(int month, int year)
        {
            ViewBag.MaPhongList = new SelectList(_context.Phongs, "MaPhong", "MaPhong");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(HoaDonCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                // 1. Sinh mã điện nước mới
                string newMaDn = "DN001";
                var lastDn = await _context.Diennuocs.OrderByDescending(dn => dn.MaDn).FirstOrDefaultAsync();
                if (lastDn != null)
                {
                    string soCuoi = lastDn.MaDn.Substring(2);
                    int soMoi = int.Parse(soCuoi) + 1;
                    newMaDn = "DN" + soMoi.ToString("D3");
                }

                // 2. Tạo bản ghi Điện Nước từ dữ liệu người dùng nhập
                var diennuoc = new Diennuoc
                {
                    MaDn = newMaDn,
                    MaPhong = model.MaPhong,
                    Sodien = model.Sodien,
                    Sonuoc = model.Sonuoc,
                    Giadien = model.Giadien,
                    Gianuoc = model.Gianuoc,
                    Ngaytao = model.Ngaytao
                };

                _context.Diennuocs.Add(diennuoc); // ✅ thêm vào bảng Diennuoc

                // 3. Tính tiền điện, tiền nước
                var tienD = (model.Sodien ?? 0) * (model.Giadien ?? 0);
                var tienNc = (model.Sonuoc ?? 0) * (model.Gianuoc ?? 0);

                // 4. Sinh mã hóa đơn mới
                string newMaHoaDon = "HD001";
                var lastHoaDon = await _context.HoaDons.OrderByDescending(h => h.MaHoaDon).FirstOrDefaultAsync();
                if (lastHoaDon != null)
                {
                    string soCuoi = lastHoaDon.MaHoaDon.Substring(2);
                    int soMoi = int.Parse(soCuoi) + 1;
                    newMaHoaDon = "HD" + soMoi.ToString("D3");
                }

                // 5. Tạo hóa đơn tương ứng với MaDn mới tạo
                var hoaDon = new HoaDon
                {
                    MaHoaDon = newMaHoaDon,
                    MaDn = newMaDn,
                    Giadien = model.Giadien,
                    Gianuoc = model.Gianuoc,
                    TienD = tienD,
                    TienNc = tienNc,
                    Ngaytao = model.Ngaytao
                };

                _context.HoaDons.Add(hoaDon); // ✅ thêm vào bảng HoaDon

                // 6. Lưu cả 2 bản ghi vào CSDL
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            // Nếu có lỗi thì load lại View
            ViewBag.MaPhongList = new SelectList(_context.Phongs, "MaPhong", "MaPhong");
            return View(model);
        }


        public IActionResult Edit(string id)
        {
            var diennuoc = _context.Diennuocs.FirstOrDefault(d => d.MaDn == id);
            if (diennuoc == null)
            {
                return NotFound();
            }

            var viewModel = new HoaDonCreateViewModel
            {
                MaDn = diennuoc.MaDn,
                MaPhong = diennuoc.MaPhong,
                Sodien = diennuoc.Sodien,
                Sonuoc = diennuoc.Sonuoc,
                Giadien = diennuoc.Giadien,
                Gianuoc = diennuoc.Gianuoc,
                Ngaytao = diennuoc.Ngaytao
            };

            ViewBag.MaPhongList = new SelectList(_context.Phongs, "MaPhong", "MaPhong"); // nếu cần
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(HoaDonCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.MaPhongList = new SelectList(_context.Phongs, "MaPhong", "MaPhong");
                return View(model);
            }

            // Cập nhật thông tin trong bảng DIENNUOC
            var dn = _context.Diennuocs.FirstOrDefault(d => d.MaDn == model.MaDn);
            if (dn == null)
            {
                return NotFound();
            }

            dn.MaPhong = model.MaPhong;
            dn.Sodien = model.Sodien;
            dn.Sonuoc = model.Sonuoc;
            dn.Giadien = model.Giadien;
            dn.Gianuoc = model.Gianuoc;
            dn.Ngaytao = model.Ngaytao;

            // Tính tiền điện và nước mới
            int tienDien = (model.Sodien ?? 0) * (model.Giadien ?? 0);
            int tienNuoc = (model.Sonuoc ?? 0) * (model.Gianuoc ?? 0);

            // Cập nhật vào bảng HOADON
            var hoadon = _context.HoaDons.FirstOrDefault(h => h.MaDn == model.MaDn);
            if (hoadon != null)
            {
                hoadon.TienD = tienDien;
                hoadon.TienNc = tienNuoc;
                hoadon.Ngaytao = model.Ngaytao ?? DateTime.Now;
            }

            _context.SaveChanges();
            return RedirectToAction("Index");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(string id)
        {
            var hoadon = _context.HoaDons.FirstOrDefault(h => h.MaHoaDon == id);
            if (hoadon == null)
            {
                return NotFound();
            }

            var diennuoc = _context.Diennuocs.FirstOrDefault(d => d.MaDn == hoadon.MaDn);

            _context.HoaDons.Remove(hoadon);
            if (diennuoc != null)
            {
                _context.Diennuocs.Remove(diennuoc);
            }

            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Detail(string id)
        {
            var hoadon = _context.HoaDons.FirstOrDefault(h => h.MaHoaDon == id);
            if (hoadon == null)
                return NotFound();

            var diennuoc = _context.Diennuocs.FirstOrDefault(d => d.MaDn == hoadon.MaDn);
            if (diennuoc == null)
                return NotFound();

            var viewModel = new HoaDonCreateViewModel
            {
                MaHoaDon = hoadon.MaHoaDon,
                MaDn = hoadon.MaDn,
                MaPhong = diennuoc.MaPhong,
                Sodien = diennuoc.Sodien,
                Giadien = diennuoc.Giadien,
                Sonuoc = diennuoc.Sonuoc,
                Gianuoc = diennuoc.Gianuoc,
                Ngaytao = diennuoc.Ngaytao
            };

            return View(viewModel);
        }
    }
}
