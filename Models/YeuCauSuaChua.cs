using System;
using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#nullable disable

namespace Quanlykytucxa.Models
{
    public partial class YeuCauSuaChua
    {
        [Key]
        public int MaYeuCau { get; set; }

        [Required]
        [ForeignKey("SinhVien")]
        public string SinhVienId { get; set; }

        public int? MaPhong { get; set; }

        public string Ghichu { get; set; }

        public string TrangThai { get; set; }

        // Navigation property nên đặt trùng với tên FK là SinhVien
        public virtual SinhVien SinhVien { get; set; }

        public virtual Phong MaPhongNavigation { get; set; }
    }

}
