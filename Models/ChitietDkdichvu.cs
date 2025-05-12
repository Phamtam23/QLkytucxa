using System;
using System.Collections.Generic;

#nullable disable

namespace Quanlykytucxa.Models
{
    public partial class ChitietDkdichvu
    {
        public string MaDk { get; set; }
        public string MaDv { get; set; }
        public DateTime? Ngaydangki { get; set; }
        public int? Trangthai { get; set; }

        public virtual DangKyKtx MaDkNavigation { get; set; }
        public virtual DichvuKtx MaDvNavigation { get; set; }
    }
}
