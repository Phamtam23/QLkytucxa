using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quanlykytucxa.Models
{
    public class LoaiPhongFilterViewModel
    {
        public bool? CoMayLanh { get; set; }
        public bool? CoNauAn { get; set; }
        public int? SoLuong { get; set; }
        public List<LoaiPhong> DanhSachLoaiPhong { get; set; }
    }
}
