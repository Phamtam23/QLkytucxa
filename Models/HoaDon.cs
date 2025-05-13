using System;
using System.Collections.Generic;

#nullable disable

namespace Quanlykytucxa.Models
{
    public partial class HoaDon
    {
        public string MaHoaDon { get; set; }
        public string MaDn { get; set; }
        public int? Giadien { get; set; }
        public int? Gianuoc { get; set; }
        public int TienD { get; set; }
        public int TienNc { get; set; }
        public DateTime? Ngaytao { get; set; }

        public virtual Diennuoc MaDnNavigation { get; set; }
    }
}
