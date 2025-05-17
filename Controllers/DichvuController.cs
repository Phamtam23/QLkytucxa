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
            var userId = Request.Cookies["UserId"];
            var sinhVien = _context.SinhViens
         .Include(sv => sv.DangKyKtxes.Where(dk => dk.TrangThai == "Hoạt động"))
         .ThenInclude(dk => dk.ChitietDkdichvus) // giả sử mối quan hệ ChiTietDichVus trong DangKyKtx
         .FirstOrDefault(sv => sv.Id == userId);

            if (sinhVien == null)
            {
                return RedirectToAction("Index");
            }
            var dangKyKtxHoatDong = sinhVien.DangKyKtxes.FirstOrDefault();
            bool daDangKyDichVu = _context.ChitietDkdichvus.Any(ct => ct.MaDk == dangKyKtxHoatDong.MaDk && ct.MaDv == dichVuId);
            if (!daDangKyDichVu)
            {
                // Thêm mới chi tiết dịch vụ
                var chiTietDichVu = new ChitietDkdichvu
                {
                    MaDk = dangKyKtxHoatDong.MaDk,
                    MaDv = dichVuId,
                    Ngaydangki = DateTime.Now,
                    Trangthai=0
                };

                _context.ChitietDkdichvus.Add(chiTietDichVu);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
