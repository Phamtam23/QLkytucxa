using System;
using System.Collections.Generic;

#nullable disable

namespace Quanlykytucxa
{
    public partial class Phong
    {
        public Phong()
        {
            DangKyKtxes = new HashSet<DangKyKtx>();
            Diennuocs = new HashSet<Diennuoc>();
            YeuCauSuaChuas = new HashSet<YeuCauSuaChua>();
        }

        public int MaPhong { get; set; }
        public int MaKhu { get; set; }
        public int SoluongSv { get; set; }
        public int? Soluongdk { get; set; }
        public int? Tienphong { get; set; }
        public string Mota { get; set; }
        public int? Loaiphong { get; set; }
        public int? Trangthai { get; set; }

        public virtual Khu MaKhuNavigation { get; set; }
        public virtual ICollection<DangKyKtx> DangKyKtxes { get; set; }
        public virtual ICollection<Diennuoc> Diennuocs { get; set; }
        public virtual ICollection<YeuCauSuaChua> YeuCauSuaChuas { get; set; }
    }
}
