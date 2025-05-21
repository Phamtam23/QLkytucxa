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
        public string TieuDe { get; set; }

        [Required]
        [StringLength(255)]
        public string NoiDung { get; set; }

        public SelectList SinhViens { get; set; } // Dùng để bind dropdown
    }
}
