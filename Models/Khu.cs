using System;
using System.Collections.Generic;

#nullable disable

namespace Quanlykytucxa
{
    public partial class Khu
    {
        public Khu()
        {
            Phongs = new HashSet<Phong>();
        }

        public int MaKhu { get; set; }
        public string TenKhu { get; set; }

        public virtual ICollection<Phong> Phongs { get; set; }
    }
}
