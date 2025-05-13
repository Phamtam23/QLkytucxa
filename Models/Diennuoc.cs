using System;
using System.Collections.Generic;

#nullable disable

namespace Quanlykytucxa.Models
{
    public partial class Diennuoc
    {
        public string MaDn { get; set; }
        public string TransId { get; set; }
        public int? MaPhong { get; set; }
        public int? Sodien { get; set; }
        public int? Sonuoc { get; set; }
        public int? Giadien { get; set; }
        public int? Gianuoc { get; set; }
        public DateTime? Ngaytao { get; set; }

        public virtual Phong MaPhongNavigation { get; set; }
        public virtual HoaDon HoaDon { get; set; }
    }
}
