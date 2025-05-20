using System.Collections.Generic;

namespace Quanlykytucxa.Models
{
    public class PhongDetailViewModel
    {
        public int MaPhong { get; set; }
        public string TenPhong { get; set; }
        public List<SinhVienViewModel> SinhVienTrongPhong { get; set; }
        public int SoLuongDangKy { get; set; }
    }
}
