using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Quanlykytucxa.Models;
using Quanlykytucxa.Models.Momo;
using RestSharp;
using Quanlykytucxa.Services;
using Quanlykytucxa.Services.Momo;

namespace Quanlykytucxa.Controllers
{
    public class PaymentController : Controller
    {
        private IMomoservice _momoService;

        public PaymentController(IMomoservice momoservice)
        {
            _momoService = momoservice;
        }
        [HttpPost]
        public async Task<IActionResult> CreatePaymentURL(OrderInforModel model)
        {
            var response = await _momoService.CreatePaymentMomo(model);
            if (response == null || string.IsNullOrEmpty(response.PayUrl))
            {
                // Ghi log nếu cần:
                Console.WriteLine("Lỗi: Không nhận được PayUrl từ MoMo.");
                Console.WriteLine("MoMo Response: " + response);
                return BadRequest("Không thể tạo URL thanh toán. Vui lòng thử lại.");
            }
            return Redirect(response.PayUrl);
        }
        [HttpGet]
        public IActionResult PaymentCallBack()
        {
            var response = _momoService.PaymentExecuteAsync(HttpContext.Request.Query);
            return View(response);
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
