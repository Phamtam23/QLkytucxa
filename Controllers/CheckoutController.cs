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
        private readonly QuanLyKTXContext _context;
        public CheckoutController(IMomoservice momoService, QuanLyKTXContext context)
        {
            _context = context;
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
            var part = response.OrderInfo.Split('|');
            if (part[1].Equals("Nội dung:Thanh toán tiền phòng"))
            {
                var dangKyKtxHoatDong = _context.DangKyKtxes
                    .FirstOrDefault(dk => dk.SinhVienId == model.extraData
                                       && (dk.TrangThai == "Đang chờ xử lý"));
                if (model.errorCode == "0") // Thanh toán thành công
                {

                    dangKyKtxHoatDong.TrangThai = "Đang hoạt động";
                    dangKyKtxHoatDong.TransId = model.transId;
                    dangKyKtxHoatDong.Ngaythanhtoan = DateTime.Now;
                    _context.DangKyKtxes.Update(dangKyKtxHoatDong);
                    await _context.SaveChangesAsync();
                }

                else
                {

                    return RedirectToAction("Index", "Lichsudatphong", new { area = "" });

                }

            }
            else
                if (part[1].Equals("Nội dung:Thanh toán dịch vụ"))
            {
                ChitietDkdichvu ct = _context.ChitietDkdichvus.FirstOrDefault(ct => ct.MaDk == part[2]&&ct.MaDv==part[3]);
                if (ct != null)
                {
                    ct.Trangthai =1;
                    _context.ChitietDkdichvus.Update(ct);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    Diennuoc dn = _context.Diennuocs.FirstOrDefault(dn => dn.MaDn == part[2]);
                    dn.TransId = model.transId;
                    _context.Diennuocs.Update(dn);
                    await _context.SaveChangesAsync();
                }    

            }
            



            return View(response); // Trả về View hiển thị thông tin thanh toán
        }

    }
}
