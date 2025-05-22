using System;

namespace Quanlykytucxa.Models
{
    public class HoaDonCreateViewModel
    {
        public string MaHoaDon { get; set; }

        // Liên kết với điện nước
        public string MaDn { get; set; }
        public int? MaPhong { get; set; }

        public int? Sodien { get; set; }
        public int? Sonuoc { get; set; }
        public int? Giadien { get; set; }
        public int? Gianuoc { get; set; }

        public DateTime? Ngaytao { get; set; } = DateTime.Now;
    }
}
