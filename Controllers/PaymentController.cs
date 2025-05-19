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
        private readonly QuanLyKTXContext _context;
        public PaymentController(IMomoservice momoservice, QuanLyKTXContext context)
        {
            this._context = context;
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

        [HttpPost]
        public async Task<IActionResult> MomoNotify([FromBody] MomoInformodel data)
        {
            Console.WriteLine("vào");
            var dangKyKtxHoatDong = _context.DangKyKtxes
                .FirstOrDefault(dk => dk.SinhVienId == data.extraData
                                   && (dk.TrangThai == "Đang chờ xử lý"||dk.TrangThai=="Thanh toán thất bại"));

            if (!_momoService.IsValidSignature(data))
            {
                return BadRequest("Chữ ký không hợp lệ");
            }
            var rawHash = $"amount={data.amount}&extraData={data.extraData}&message={data.message}" +
                        $"&orderId={data.orderId}&orderInfo={data.orderInfo}&orderType={data.orderType}&partnerCode={data.partnerCode}" +
                        $"&payType={data.payType}&requestId={data.requestId}&responseTime={data.responseTime}&resultCode={data.errorCode}&transId={data.transId}";
            if (data.errorCode=="0")
            {
                dangKyKtxHoatDong.TrangThai = "Đang hoạt động";
                dangKyKtxHoatDong.TransId = rawHash;
                dangKyKtxHoatDong.Ngaythanhtoan = DateTime.Now;
                _context.DangKyKtxes.Update(dangKyKtxHoatDong);
                await _context.SaveChangesAsync();
            }    
            else

            {
                dangKyKtxHoatDong.TrangThai = "Thanh toán thất bại";
                _context.DangKyKtxes.Update(dangKyKtxHoatDong);
                await _context.SaveChangesAsync();

            }
            return Ok("Notification received");
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
