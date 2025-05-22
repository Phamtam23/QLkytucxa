using Microsoft.AspNetCore.Mvc;
using Quanlykytucxa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quanlykytucxa.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class YeucauController : Controller
    {
        private readonly QuanLyKTXContext _context;

        public YeucauController(QuanLyKTXContext context)
        {
            _context = context;
        }
        [HttpPost]
        public IActionResult Hoanthanh(int mayeucau)
        {
            var yc = _context.YeuCauSuaChuas.FirstOrDefault(yc => yc.MaYeuCau == mayeucau);
            yc.TrangThai = "Đã xử lý";
            _context.YeuCauSuaChuas.Update(yc);
            _context.SaveChanges();
            return RedirectToAction("Index","Yeucau");
        }    
        public IActionResult Index(int? thang, int? nam, string trangthai)
        {
            int currentMonth = DateTime.Now.Month;
            int currentYear = DateTime.Now.Year;

            int filterThang = thang ?? currentMonth;
            int filterNam = nam ?? currentYear;

            var query = _context.YeuCauSuaChuas.AsQueryable();

            query = query.Where(x => x.Ngaygui.Month == filterThang && x.Ngaygui.Year == filterNam);

            if (!string.IsNullOrEmpty(trangthai))
            {
                query = query.Where(x => x.TrangThai == trangthai);
            }

            var model = query.ToList();

            ViewBag.thang = filterThang;
            ViewBag.nam = filterNam;
            ViewBag.trangthai = trangthai;

            return View(model);
        }
    }
}
