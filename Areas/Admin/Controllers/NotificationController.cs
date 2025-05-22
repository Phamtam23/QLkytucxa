using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Quanlykytucxa.Models.ViewModels;
using Quanlykytucxa.Models;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Quanlykytucxa.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    [Area("Admin")]
    public class NotificationController : Controller
    {
        private readonly QuanLyKTXContext _context;
        private readonly Random _random = new Random();

        public NotificationController(QuanLyKTXContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var viewModel = new SendNotificationViewModel
            {
                SinhViens = new SelectList(_context.SinhViens, "Id", "TenSv")
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(SendNotificationViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Xóa các thông báo cũ hơn 30 ngày
                var oldNotifications = _context.ThongBaos
                    .Where(tb => tb.NgayDang < DateTime.Now.AddDays(-30));
                _context.ThongBaos.RemoveRange(oldNotifications);

                if (model.SinhVienId == "all")
                {
                    var allSinhViens = _context.SinhViens.ToList();
                    foreach (var sv in allSinhViens)
                    {
                        int maTB;
                        do
                        {
                            maTB = _random.Next(100000, 999999);
                        } while (_context.ThongBaos.Any(tb => tb.MaThongBao == maTB));

                        _context.ThongBaos.Add(new ThongBao
                        {
                            MaThongBao = maTB,
                            SinhVienId = sv.Id,
                            TieuDe = model.TieuDe,
                            NoiDung = model.NoiDung,
                            NgayDang = DateTime.Now
                        });
                    }
                }
                else
                {
                    int maTB;
                    do
                    {
                        maTB = _random.Next(100000, 999999);
                    } while (_context.ThongBaos.Any(tb => tb.MaThongBao == maTB));

                    _context.ThongBaos.Add(new ThongBao
                    {
                        MaThongBao = maTB,
                        SinhVienId = model.SinhVienId,
                        TieuDe = model.TieuDe,
                        NoiDung = model.NoiDung,
                        NgayDang = DateTime.Now
                    });
                }

                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Thông báo đã được gửi!";
                return RedirectToAction("Index");
            }

            // Nếu ModelState không hợp lệ, gán lại SelectList
            model.SinhViens = new SelectList(_context.SinhViens, "Id", "TenSv", model.SinhVienId);
            return View(model);
        }
    }
}
