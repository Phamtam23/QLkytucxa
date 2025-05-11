using System;
using System.Collections.Generic;

#nullable disable

namespace Quanlykytucxa
{
    public partial class DangKyKtx
    {
        public string MaDk { get; set; }
        public string MaSv { get; set; }
        public int? MaPhong { get; set; }
        public DateTime NgayDangKy { get; set; }
        public DateTime? NgayKetThuc { get; set; }
        public string TrangThai { get; set; }
        public string GhiChu { get; set; }
        public DateTime? Ngaythanhtoan { get; set; }

        public virtual Phong MaPhongNavigation { get; set; }
        public virtual SinhVien MaSvNavigation { get; set; }
    }
}
