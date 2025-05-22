using Microsoft.AspNetCore.Mvc;
using Quanlykytucxa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quanlykytucxa.Controllers
{
    public class SuachuaController : Controller
    {
        private readonly QuanLyKTXContext _context;

        public SuachuaController(QuanLyKTXContext context)
        {
            this._context = context;
        }
        public IActionResult Index()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var dk=_context.DangKyKtxes.FirstOrDefault(dk => dk.SinhVienId == userId && (dk.TrangThai == "Đang hoạt động" || dk.TrangThai == "Đang chờ xử lý"));
            var dssuachua = _context.YeuCauSuaChuas
                .Where(sc => sc.MaPhong == dk.MaPhong&&sc.Ngaygui<=dk.NgayKetThuc&&sc.Ngaygui>=dk.NgayDangKy)
                .ToList();

            return View(dssuachua);
        }

        [HttpPost]
        public IActionResult Addyeucau(string noidung,string soluong)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account", new { area = "" });
            }

            var dk = _context.DangKyKtxes
                .FirstOrDefault(dk => dk.SinhVienId == userId &&
                                      (dk.TrangThai == "Đang hoạt động" || dk.TrangThai == "Đang chờ xử lý"));

            if (dk == null)
            {
                TempData["Alert"] = "Bạn chưa có đăng ký KTX hợp lệ.";
                return RedirectToAction("Index", "Home",new { area=""});
            }
            string maStr = DateTime.Now.ToString("yyMMddHHmm");
            int MaYeuCau = int.Parse(DateTime.Now.ToString("MMddHHmm"));
            var item = new YeuCauSuaChua
            {
                MaYeuCau = MaYeuCau,
                Ghichu = noidung + "|" + soluong,
                MaPhong = dk.MaPhong,
                SinhVienId = userId,
                Ngaygui = DateTime.Now,
                TrangThai = "Chờ xử lý"
            };
            _context.YeuCauSuaChuas.Add(item);
            _context.SaveChanges();

            TempData["Alert"] = "Thêm yêu cầu thành công!";
            return RedirectToAction("Index", "Suachua",new { area="" });
        }
    }
}
