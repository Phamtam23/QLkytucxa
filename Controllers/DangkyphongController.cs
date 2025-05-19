using Microsoft.AspNetCore.Mvc;
using Quanlykytucxa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quanlykytucxa.Controllers
{

    
    public class DangkyphongController : Controller
    {
        private readonly QuanLyKTXContext _context;
        public DangkyphongController(QuanLyKTXContext context)
        {
            this._context = context;
        }

        public IActionResult Index(bool? coMayLanh, bool? coNauAn, int? soLuong)
        {
            var query = _context.LoaiPhongs.AsQueryable();

            if (coMayLanh == true)
                query = query.Where(lp => !lp.Tenloai.Contains("Không|"));

            if (coNauAn == true)
                query = query.Where(lp => !lp.Tenloai.EndsWith("|Không"));

            if (soLuong.HasValue)
                query = query.Where(lp => lp.SoluongSv == soLuong.Value);

            ViewBag.CoMayLanh = coMayLanh ?? false;
            ViewBag.CoNauAn = coNauAn ?? false;
            ViewBag.SoLuong = soLuong;

            return View(query.ToList());
        }


        public IActionResult DetailDK(int maloai,string maylanh,string nauan)
        {
            ViewBag.ml = maylanh;
            ViewBag.na = nauan;
            var dsphong = _context.Phongs.Where(p => p.Maloai == maloai).ToList();

            return View(dsphong);
        }
        [HttpPost]
        public IActionResult DangKyKTX(int maphong)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var gioiTinh = _context.SinhViens
                .Where(sv => sv.Id == userId)
                .Select(sv => sv.GioiTinh)
                .FirstOrDefault();
            string gt = (maphong - 300) > 0 ? "Nam" : "Nữ";
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account",new {area=""});
            }
            var phong = _context.Phongs.FirstOrDefault(p => p.MaPhong == maphong);
            var sinhvien = _context.SinhViens.FirstOrDefault(sv => sv.Id== userId);
            bool dkphong = _context.DangKyKtxes.Any(dk => dk.SinhVienId == userId && (dk.TrangThai == "Hoạt động" || dk.TrangThai == "Đang chờ xử lý"));


            if (gioiTinh==gt)
            {
                if (dkphong)
                {
                    TempData["Alert"] = "Thất bại , bạn đã có phòng trước đó rồi";
                    return RedirectToAction("Index", "Dangkyphong", new { area = "" });
                }
                else
                {
                    var dk = new DangKyKtx
                    {
                        MaDk = Guid.NewGuid().ToString("N").Substring(0, 10),
                        SinhVienId = userId,
                        MaPhong = maphong,
                        MaPhongNavigation = phong,
                        SinhVien = sinhvien,
                        TransId = "",
                        GhiChu = "",
                        NgayDangKy = DateTime.Now,
                        NgayKetThuc = DateTime.Now.AddMonths(5),
                        TrangThai = "Đang chờ xử lý"
                    };
                    _context.DangKyKtxes.Add(dk);
                    _context.SaveChanges();
                    return RedirectToAction("DetailLsdatphong", "Lichsudatphong", new { madk = dk.MaDk, area = "" });
                }
            }
            else
            {
         
                TempData["Alert"] = "Vui lòng chọn đúng giới tính";
                return RedirectToAction("DetailDK", "Dangkyphong", new { area = "" ,maloai=phong.Maloai});
            }    
            
           
        }
    }
}
