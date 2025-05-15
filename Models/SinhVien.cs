using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
#nullable disable

namespace Quanlykytucxa.Models
{
    public partial class SinhVien:IdentityUser
    {
        public SinhVien()
        {
            DangKyKtxes = new HashSet<DangKyKtx>();
            ThongBaos = new HashSet<ThongBao>();
            YeuCauSuaChuas = new HashSet<YeuCauSuaChua>();
        }
        public string TenSv { get; set; }
        public DateTime NgaySinh { get; set; }
        public string GioiTinh { get; set; }
        public string Sdt { get; set; }
        public string Cccd { get; set; }
        public string DiaChi { get; set; }
        public virtual ICollection<DangKyKtx> DangKyKtxes { get; set; }
        public virtual ICollection<ThongBao> ThongBaos { get; set; }
        public virtual ICollection<YeuCauSuaChua> YeuCauSuaChuas { get; set; }
    }
}
