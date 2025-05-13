using System;
using System.Collections.Generic;

#nullable disable

namespace Quanlykytucxa.Models
{
    public partial class DichvuKtx
    {
        public DichvuKtx()
        {
            ChitietDkdichvus = new HashSet<ChitietDkdichvu>();
        }

        public string MaDv { get; set; }
        public int? Giadichvu { get; set; }
        public string Ghichu { get; set; }

        public virtual ICollection<ChitietDkdichvu> ChitietDkdichvus { get; set; }
    }
}
