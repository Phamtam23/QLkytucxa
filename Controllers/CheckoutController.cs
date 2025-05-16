using Microsoft.AspNetCore.Mvc;
using Quanlykytucxa.Services.Momo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Quanlykytucxa.Models;
namespace Quanlykytucxa.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly IMomoservice _momoService;
        public CheckoutController(IMomoservice momoService)
        {
            _momoService = momoService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult PaymentCallBack()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> PaymentCallBack([FromQuery] MomoInformodel model)
        {
            var requestQuery = HttpContext.Request.Query;
            var response =  _momoService.PaymentExecuteAsync(HttpContext.Request.Query);
            Console.WriteLine(response);
           /* if (requestQuery["resultCode"] != "0") // Thanh toán thành công
            {
                var newMomoInsert = new MomoInfoModel
                {
                    OrderId = requestQuery["orderId"],
                    FullName = User.FindFirstValue(ClaimTypes.Email),
                    Amount = decimal.Parse(requestQuery["amount"]),
                    OrderInfo = requestQuery["orderInfo"],
                    DatePaid = DateTime.Now
                };

                _dataContext.Add(newMomoInsert);
                await _dataContext.SaveChangesAsync();
            }
            else
            {
                TempData["success"] = "Đã hủy giao dịch Momo.";
                return RedirectToAction("Index", "Cart");
            }
*/
            return View(response); // Trả về View hiển thị thông tin thanh toán
        }

    }
}
