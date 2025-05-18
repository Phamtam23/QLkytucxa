using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Quanlykytucxa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quanlykytucxa.Controllers
{
    public class LichsudatphongController : Controller
    {
        private readonly QuanLyKTXContext _context;

        public LichsudatphongController (QuanLyKTXContext context)
        {
           this._context = context;
        }
        public IActionResult Index()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account");
            }
            var dslsdatphong = _context.DangKyKtxes
            .Where(dk => dk.SinhVienId == userId)
            .Include(dk => dk.MaPhongNavigation)
                .ThenInclude(p => p.MaloaiNavigation) // Nếu cần hiện loại phòng
            .ToList();
            return View(dslsdatphong);
        }
        public IActionResult DetailLsdatphong(string madk)
        {

          var detailsdphong = _context.DangKyKtxes
        .Include(dk => dk.MaPhongNavigation)
            .ThenInclude(p => p.MaloaiNavigation)
        .Include(dk => dk.ChitietDkdichvus)           // include dịch vụ đăng ký
            .ThenInclude(ct => ct.MaDvNavigation)     // include chi tiết dịch vụ (tên, giá,...)
        .FirstOrDefault(dk => dk.MaDk == madk);


            if (detailsdphong == null)
            {
                return NotFound();
            }

            return View(detailsdphong);
        }

    }
}
