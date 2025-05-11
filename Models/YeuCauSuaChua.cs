using System;
using System.Collections.Generic;

#nullable disable

namespace Quanlykytucxa
{
    public partial class YeuCauSuaChua
    {
        public int MaYeuCau { get; set; }
        public string MaSv { get; set; }
        public int? MaPhong { get; set; }
        public string Ghichu { get; set; }
        public string TrangThai { get; set; }

        public virtual Phong MaPhongNavigation { get; set; }
        public virtual SinhVien MaSvNavigation { get; set; }
    }
}
