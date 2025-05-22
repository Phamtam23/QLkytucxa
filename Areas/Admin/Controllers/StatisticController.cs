using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Quanlykytucxa.Models;
using Quanlykytucxa.ViewModels;
using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Quanlykytucxa.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    [Area("Admin")]
    public class StatisticController : Controller
    {
        private readonly QuanLyKTXContext _context;

        public StatisticController(QuanLyKTXContext context)
        {
            _context = context;
        }


        public async Task<decimal> Tongdanhthu()
        {
            var tongTienDangKy = await _context.DangKyKtxes
                .Include(dk => dk.MaPhongNavigation)
                .Where(dk => dk.MaPhongNavigation != null)
                .SumAsync(dk => dk.MaPhongNavigation.Tienphong ?? 0);
            var tongdiennuoc = await _context.Diennuocs
                .SumAsync(dn => dn.Sodien * dn.Giadien + dn.Sonuoc * dn.Gianuoc ?? 0);
            var tongTienDichVu = await _context.ChitietDkdichvus
                .Include(ct => ct.MaDvNavigation)
                .Where(ct => ct.MaDvNavigation != null)
                .SumAsync(ct => ct.MaDvNavigation.Giadichvu ?? 0);

            return tongTienDangKy + tongTienDichVu + tongdiennuoc;
        }


        public async Task<decimal> TongDoanhThuNamHienTai()
        {
            var now = DateTime.Now;
            var from = new DateTime(now.Year, 1, 1);
            var to = from.AddYears(1);

            var tongTienDangKy = await _context.DangKyKtxes
                .Include(dk => dk.MaPhongNavigation)
                .Where(dk => dk.NgayDangKy >= from && dk.NgayDangKy < to && dk.MaPhongNavigation != null)
                .SumAsync(dk => dk.MaPhongNavigation.Tienphong ?? 0);

            var tongTienDienNuoc = await _context.Diennuocs
                .Where(dn => dn.Ngaytao >= from && dn.Ngaytao < to)
                .SumAsync(dn => (dn.Sodien * dn.Giadien + dn.Sonuoc * dn.Gianuoc) ?? 0);

            var tongTienDichVu = await _context.ChitietDkdichvus
                .Include(ct => ct.MaDvNavigation)
                .Where(ct => ct.Ngaydangki >= from && ct.Ngaydangki < to && ct.MaDvNavigation != null)
                .SumAsync(ct => ct.MaDvNavigation.Giadichvu ?? 0);

            return tongTienDangKy + tongTienDienNuoc + tongTienDichVu;
        }

        public async Task<decimal> TongDoanhThuThangHienTai()
        {
            var now = DateTime.Now;
            var from = new DateTime(now.Year, now.Month, 1);
            var to = from.AddMonths(1);

            var tongTienDangKy = await _context.DangKyKtxes
                .Include(dk => dk.MaPhongNavigation)
                .Where(dk => dk.NgayDangKy >= from && dk.NgayDangKy < to && dk.MaPhongNavigation != null)
                .SumAsync(dk => dk.MaPhongNavigation.Tienphong ?? 0);

            var tongTienDienNuoc = await _context.Diennuocs
                .Where(dn => dn.Ngaytao >= from && dn.Ngaytao < to)
                .SumAsync(dn => (dn.Sodien * dn.Giadien + dn.Sonuoc * dn.Gianuoc) ?? 0);

            var tongTienDichVu = await _context.ChitietDkdichvus
                .Include(ct => ct.MaDvNavigation)
                .Where(ct => ct.Ngaydangki >= from && ct.Ngaydangki < to && ct.MaDvNavigation != null)
                .SumAsync(ct => ct.MaDvNavigation.Giadichvu ?? 0);

            return tongTienDangKy + tongTienDienNuoc + tongTienDichVu;
        }

        [HttpGet]
        public async Task<List<Thongke>> GetRevenueByYears()
        {
            var years = await _context.DangKyKtxes
                .Select(dk => dk.NgayDangKy.Year)
                .Union(_context.ChitietDkdichvus.Select(ct => ct.Ngaydangki.Value.Year))
                .Union(_context.Diennuocs.Select(dn => dn.Ngaytao.Value.Year))
                .Distinct()
                .OrderBy(y => y)
                .ToListAsync();

            var results = new List<Thongke>();

            foreach (var year in years)
            {
                var from = new DateTime(year, 1, 1);
                var to = from.AddYears(1);

                var tienPhong = await _context.DangKyKtxes
                    .Include(dk => dk.MaPhongNavigation)
                    .Where(dk => dk.NgayDangKy >= from && dk.NgayDangKy < to && dk.MaPhongNavigation != null)
                    .SumAsync(dk => dk.MaPhongNavigation.Tienphong ?? 0);

                var tienDichVu = await _context.ChitietDkdichvus
                    .Include(ct => ct.MaDvNavigation)
                    .Where(ct => ct.Ngaydangki >= from && ct.Ngaydangki < to && ct.MaDvNavigation != null)
                    .SumAsync(ct => ct.MaDvNavigation.Giadichvu ?? 0);

                var tienDienNuoc = await _context.Diennuocs
                    .Where(dn => dn.Ngaytao >= from && dn.Ngaytao < to)
                    .SumAsync(dn => (dn.Sodien * dn.Giadien + dn.Sonuoc * dn.Gianuoc) ?? 0);

                results.Add(new Thongke
                {
                    label = $"Năm {year}",
                    sotien = tienPhong + tienDichVu + tienDienNuoc
                });
            }

            return results;
        }

        [HttpGet]
        public async Task<List<Thongke>> GetRevenueByMonthsInYear(int year)
        {
            var result = new List<Thongke>();

            for (int month = 1; month <= 12; month++)
            {
                var from = new DateTime(year, month, 1);
                var to = from.AddMonths(1);

                var tienPhong = await _context.DangKyKtxes
                    .Include(dk => dk.MaPhongNavigation)
                    .Where(dk => dk.NgayDangKy >= from && dk.NgayDangKy < to && dk.MaPhongNavigation != null)
                    .SumAsync(dk => dk.MaPhongNavigation.Tienphong ?? 0);

                var tienDichVu = await _context.ChitietDkdichvus
                    .Include(ct => ct.MaDvNavigation)
                    .Where(ct => ct.Ngaydangki >= from && ct.Ngaydangki < to && ct.MaDvNavigation != null)
                    .SumAsync(ct => ct.MaDvNavigation.Giadichvu ?? 0);

                var tienDienNuoc = await _context.Diennuocs
                    .Where(dn => dn.Ngaytao.Value >= from && dn.Ngaytao.Value < to)
                    .SumAsync(dn => (dn.Sodien * dn.Giadien + dn.Sonuoc * dn.Gianuoc) ?? 0);

                result.Add(new Thongke
                {
                    label = $"Tháng {month}",
                    sotien = tienPhong + tienDichVu + tienDienNuoc
                });
            }

            return result;
        }


        public async Task<List<Topphongtieudien>> Top10PhongTieuThuDienNuoc()
        {
            var now = DateTime.Now;
            var from = new DateTime(now.Year, now.Month, 1);
            var to = from.AddMonths(1);

            var topPhongs = await _context.Diennuocs
                .Where(dn => dn.Ngaytao >= from && dn.Ngaytao < to && dn.MaPhong != null)
                .GroupBy(dn => dn.MaPhong)
                .Select(g => new Topphongtieudien
                {
                    MaPhong = g.Key,
                    TongSoDien = g.Sum(dn => dn.Sodien ?? 0),
                    TongSoNuoc = g.Sum(dn => dn.Sonuoc ?? 0),
                    TongTien = g.Sum(dn => (dn.Sodien * dn.Giadien + dn.Sonuoc * dn.Gianuoc) ?? 0)
                })
                .OrderByDescending(p => p.TongTien)
                .Take(10)
                .ToListAsync();

            return topPhongs.Cast<Topphongtieudien>().ToList();
        }





        public async Task<IActionResult> Index()
        {
            var tknam = await GetRevenueByYears();
            var tkthang = await GetRevenueByMonthsInYear(2025);
            var topt = await Top10PhongTieuThuDienNuoc();
            var tongdoanhthu = await Tongdanhthu();
            var tongdoanhthunam = await TongDoanhThuNamHienTai();
            var tongdoanhthuthang = await TongDoanhThuThangHienTai();
            ViewBag.topdungnhieu = topt;
            ViewBag.tknam = tknam;
            ViewBag.tkthang = tkthang;
            ViewBag.Tongdoangthu = tongdoanhthu;
            ViewBag.Tongdoangthunam = tongdoanhthunam;
            ViewBag.Tongdoangthuthang = tongdoanhthuthang;
            return View();
        }

    }
}
