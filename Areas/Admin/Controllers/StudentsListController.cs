using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Quanlykytucxa.Models;
using System.Linq;

namespace Quanlykytucxa.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class StudentsListController : Controller
    {
        /*private readonly QuanLyKTXContext _context;

        public StudentsListController(QuanLyKTXContext context)
        {
            _context = context;
        }*/

        public IActionResult Index()
        {
            /*var listSinhVien = _context.Users.OfType<SinhVien>().ToList();*/
            return View(/*listSinhVien*/);
        }

        public IActionResult Detail()
        {
            /*var listSinhVien = _context.Users.OfType<SinhVien>().ToList();*/
            return View(/*listSinhVien*/);
        }

        public IActionResult UpdateSV()
        {
            /*var listSinhVien = _context.Users.OfType<SinhVien>().ToList();*/
            return View(/*listSinhVien*/);
        }
    }
}
