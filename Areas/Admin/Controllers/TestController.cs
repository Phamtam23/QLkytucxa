using Microsoft.AspNetCore.Mvc;

namespace Quanlykytucxa.Areas.Admin.Controllers
{
    public class TestController : Controller
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
