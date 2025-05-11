using System;
using System.Collections.Generic;

#nullable disable

namespace Quanlykytucxa
{
    public partial class SinhVien
    {
        public SinhVien()
        {
            DangKyKtxes = new HashSet<DangKyKtx>();
            ThongBaos = new HashSet<ThongBao>();
            YeuCauSuaChuas = new HashSet<YeuCauSuaChua>();
        }

        public string MaSv { get; set; }
        public string TenSv { get; set; }
        public DateTime NgaySinh { get; set; }
        public string GioiTinh { get; set; }
        public string Khoa { get; set; }
        public string Lop { get; set; }
        public string EmailSv { get; set; }
        public string Sdt { get; set; }
        public string Cccd { get; set; }
        public string DiaChi { get; set; }
        public string MatKhau { get; set; }

        public virtual ICollection<DangKyKtx> DangKyKtxes { get; set; }
        public virtual ICollection<ThongBao> ThongBaos { get; set; }
        public virtual ICollection<YeuCauSuaChua> YeuCauSuaChuas { get; set; }
    }
}
