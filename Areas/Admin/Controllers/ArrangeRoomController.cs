using Microsoft.AspNetCore.Mvc;

namespace Quanlykytucxa.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ArrangeRoomController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Detail()
        {
            return View();
        }
    }
}
