using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Quanlykytucxa.Models;
using Quanlykytucxa.ViewModels;
using System;

namespace Quanlykytucxa.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class StatisticController : Controller
    {
        private readonly QuanLyKTXContext _context;

        public StatisticController(QuanLyKTXContext context)
        {
            _context = context;
        }

        public IActionResult Index(DateTime? TuNgay, DateTime? DenNgay)
        {
            var query = _context.ChitietDkdichvus
                .Where(c => (!TuNgay.HasValue || c.Ngaydangki >= TuNgay) &&
                            (!DenNgay.HasValue || c.Ngaydangki <= DenNgay))
                .Where(c => c.MaDvNavigation.Giadichvu.HasValue)
                .Select(c => new
                {
                    TenSinhVien = c.MaDkNavigation.SinhVien.TenSv,
                    TenDichVu = c.MaDvNavigation.MaDv,
                    NgayDangKy = c.Ngaydangki,
                    SoTien = c.MaDvNavigation.Giadichvu.Value
                });

            var doanhThuTheoNgay = query
                .GroupBy(x => x.NgayDangKy.Value.Date)
                .Select(g => new ThongKeTheoNgay
                {
                    Ngay = g.Key,
                    DoanhThu = g.Sum(x => x.SoTien)
                }).OrderBy(x => x.Ngay)
                .ToList();

            var giaoDichChiTiet = query
                .Select(x => new ChiTietGiaoDichDto
                {
                    TenSinhVien = x.TenSinhVien,
                    TenDichVu = x.TenDichVu,
                    NgayDangKy = x.NgayDangKy,
                    SoTien = x.SoTien
                }).ToList();

            var viewModel = new ThongKeDichVuViewModel
            {
                TuNgay = TuNgay,
                DenNgay = DenNgay,
                DoanhThuTheoNgay = doanhThuTheoNgay,
                GiaoDichChiTiet = giaoDichChiTiet,
                TongDoanhThu = giaoDichChiTiet.Sum(x => x.SoTien),
                TongLuotDangKy = giaoDichChiTiet.Count
            };

            return View(viewModel);
        }
    }
}
