using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Quanlykytucxa.Models;
using System.Linq;
using System;

namespace Quanlykytucxa.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    [Area("Admin")]
    public class ArrangeRoomController : Controller
    {
        private readonly QuanLyKTXContext _context;

        public ArrangeRoomController(QuanLyKTXContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var danhSachPhong = await _context.Phongs
                .Include(p => p.MaKhuNavigation)
                .Include(p => p.MaloaiNavigation)
                .Select(p => new PhongViewModel
                {
                    MaPhong = p.MaPhong,
                    TenKhu = p.MaKhuNavigation.TenKhu,
                    SoluongSv = p.SoluongSv,
                    Soluongdk = _context.DangKyKtxes.Count(dk => dk.MaPhong == p.MaPhong),
                    Tienphong = p.Tienphong ?? 0,
                    TenLoaiPhong = p.MaloaiNavigation.Tenloai,
                    Trangthai = p.Trangthai ?? 0
                })
                .ToListAsync();

            return View(danhSachPhong);
        }

        public IActionResult Detail(int id) // id = MaPhong
        {
            var phong = _context.Phongs
                .Include(p => p.DangKyKtxes)
                    .ThenInclude(dk => dk.SinhVien)
                .FirstOrDefault(p => p.MaPhong == id);

            if (phong == null) return NotFound();

            var sinhVienTrongPhong = phong.DangKyKtxes
                .Where(dk => dk.TrangThai == "Đã đăng ký" || dk.TrangThai == "Active")
                .Select(dk => new SinhVienViewModel
                {
                    MSSV = dk.SinhVienId,
                    HoTen = dk.SinhVien.TenSv
                })
                .ToList();

            var model = new PhongDetailViewModel
            {
                MaPhong = phong.MaPhong,
                TenPhong = $"Phòng {phong.MaPhong}",
                SinhVienTrongPhong = sinhVienTrongPhong
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddStudent(int maPhong, string mssv)
        {
            var phong = await _context.Phongs.Include(p => p.DangKyKtxes).FirstOrDefaultAsync(p => p.MaPhong == maPhong);
            if (phong == null) return NotFound();

            int soLuongDangKy = phong.DangKyKtxes.Count(dk => dk.TrangThai == "Đã đăng ký" || dk.TrangThai == "Active");
            if (soLuongDangKy >= phong.SoluongSv)
            {
                ModelState.AddModelError("", "Phòng đã đầy, không thể thêm.");
                return RedirectToAction("Detail", new { id = maPhong });
            }

            var sv = await _context.SinhViens.FirstOrDefaultAsync(s => s.Id == mssv);
            if (sv == null)
            {
                ModelState.AddModelError("", "Người dùng không tồn tại.");
                return RedirectToAction("Detail", new { id = maPhong });
            }

            var dangKy = await _context.DangKyKtxes.FirstOrDefaultAsync(d => d.SinhVienId == mssv);

            if (dangKy != null)
            {
                // Cập nhật đăng ký đã có
                dangKy.MaPhong = maPhong;
                dangKy.TrangThai = "Đã đăng ký";
            }
            else
            {
                // Tạo mới nếu chưa có
                dangKy = new DangKyKtx
                {
                    MaDk = GenerateRandomMaDK(),
                    SinhVienId = mssv,
                    MaPhong = maPhong,
                    TrangThai = "Đã đăng ký"
                };
                _context.Add(dangKy);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction("Detail", new { id = maPhong });
        }

        public static string GenerateRandomMaDK(int length = 10)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }


        [HttpPost]
        public IActionResult DeleteStudent(int maPhong, string mssv)
        {
            var dangKy = _context.DangKyKtxes.FirstOrDefault(dk => dk.MaPhong == maPhong && dk.SinhVienId == mssv);
            if (dangKy != null)
            {
                _context.DangKyKtxes.Remove(dangKy);
                _context.SaveChanges();
            }
            return RedirectToAction("Detail", new { id = maPhong });
        }
    }
}
