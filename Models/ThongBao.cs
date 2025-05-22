using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Quanlykytucxa.Models
{
    public partial class ThongBao
    {
        [Key]
        public int MaThongBao { get; set; }

        [Required]
        [ForeignKey("SinhVien")]
        public string SinhVienId { get; set; }

        [Required]
        [StringLength(50)]
        public string TieuDe { get; set; }

        public DateTime NgayDang { get; set; }

        [Required]
        [StringLength(255)]
        public string NoiDung { get; set; }

        public virtual SinhVien SinhVien { get; set; }
    }
}
