using System;
using System.Collections.Generic;

#nullable disable

namespace Quanlykytucxa.Models
{
    public partial class DangKyKtx
    {
        public DangKyKtx()
        {
            ChitietDkdichvus = new HashSet<ChitietDkdichvu>();
        }

        public string MaDk { get; set; }
        public string MaSv { get; set; }
        public int? MaPhong { get; set; }
        public string TransId { get; set; }
        public DateTime NgayDangKy { get; set; }
        public DateTime? NgayKetThuc { get; set; }
        public string TrangThai { get; set; }
        public string GhiChu { get; set; }
        public DateTime? Ngaythanhtoan { get; set; }

        public virtual Phong MaPhongNavigation { get; set; }
        public virtual SinhVien MaSvNavigation { get; set; }
        public virtual ICollection<ChitietDkdichvu> ChitietDkdichvus { get; set; }
    }
}
