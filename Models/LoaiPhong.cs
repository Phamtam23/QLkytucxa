using System;
using System.Collections.Generic;

#nullable disable

namespace Quanlykytucxa.Models
{
    public partial class LoaiPhong
    {
        public LoaiPhong()
        {
            Phongs = new HashSet<Phong>();
        }

        public int Maloai { get; set; }
        public string Tenloai { get; set; }
        public int? SoluongSv { get; set; }
        public string anh { get; set; }
        public virtual ICollection<Phong> Phongs { get; set; }
    }
}
