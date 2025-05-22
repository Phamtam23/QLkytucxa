using System;
using System.Collections.Generic;

namespace Quanlykytucxa.ViewModels
{
    public class ThongKeDichVuViewModel
    {
        public decimal TongDoanhThu { get; set; }
        public int TongLuotDangKy { get; set; }
        public List<ThongKeTheoNgay> DoanhThuTheoNgay { get; set; }
        public List<ChiTietGiaoDichDto> GiaoDichChiTiet { get; set; }

        public DateTime? TuNgay { get; set; }
        public DateTime? DenNgay { get; set; }
    }

    public class ThongKeTheoNgay
    {
        public DateTime Ngay { get; set; }
        public decimal DoanhThu { get; set; }
    }

    public class ChiTietGiaoDichDto
    {
        public string TenSinhVien { get; set; }
        public string TenDichVu { get; set; }
        public DateTime? NgayDangKy { get; set; }
        public decimal SoTien { get; set; }
    }
}
