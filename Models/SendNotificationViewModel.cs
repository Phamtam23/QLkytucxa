using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Quanlykytucxa.Models.ViewModels
{
    public class SendNotificationViewModel
    {
        public string SinhVienId { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Tiêu đề")]
        public string TieuDe { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Nội dung")]
        public string NoiDung { get; set; }

        public SelectList SinhViens { get; set; } // Dùng để bind dropdown
    }
}
