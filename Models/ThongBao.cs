using System;
using System.Collections.Generic;

#nullable disable

namespace Quanlykytucxa
{
    public partial class ThongBao
    {
        public int MaThongBao { get; set; }
        public string MaSv { get; set; }
        public string TieuDe { get; set; }
        public DateTime NgayDang { get; set; }
        public string NoiDung { get; set; }

        public virtual SinhVien MaSvNavigation { get; set; }
    }
}
