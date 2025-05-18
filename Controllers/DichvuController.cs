using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Quanlykytucxa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quanlykytucxa.Controllers
{
    public class DichvuController : Controller
    {
        private readonly QuanLyKTXContext _context;
        public DichvuController(QuanLyKTXContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var services = _context.DichvuKtxes.ToList();
            return View(services);
        }

        [HttpPost]
        public IActionResult DangKyDichVu(string dichVuId)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account");
            }
            var dangKyKtxHoatDong = _context.DangKyKtxes
                .FirstOrDefault(dk => dk.SinhVienId == userId
                                   && (dk.TrangThai == "Hoạt động" || dk.TrangThai == "Đang chờ xử lý"));


            if (dangKyKtxHoatDong == null)
            {
                TempData["Alert"] = "Bạn chưa có đăng ký KTX đang hoạt động.";
                return RedirectToAction("Index", "Dichvu",new {area=""});
            }

            bool daDangKyDichVu = _context.ChitietDkdichvus
                .Any(ct => ct.MaDk == dangKyKtxHoatDong.MaDk && ct.MaDv == dichVuId);

            if (daDangKyDichVu)
            {
                TempData["Alert"] = "Bạn đã đăng ký dịch vụ này rồi.";
                return RedirectToAction("Index", "Dichvu", new { area = "" });
            }

            // Nếu chưa có thì thêm mới
            var chiTietDichVu = new ChitietDkdichvu
            {
                MaDk = dangKyKtxHoatDong.MaDk,
                MaDv = dichVuId,
                Ngaydangki = DateTime.Now,
                Trangthai = 0
            };

            _context.ChitietDkdichvus.Add(chiTietDichVu);
            _context.SaveChanges();

            TempData["Alert"] = "Đăng ký dịch vụ thành công.";
            return RedirectToAction("Index", "Dichvu", new { area = "" });
        }

    }
}
