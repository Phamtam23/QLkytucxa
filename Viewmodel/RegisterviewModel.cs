using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace Quanlykytucxa.Viewmodel
{
    public class RegisterviewModel
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Gioi tinh is required")]
        public string Gioitinh { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(40,MinimumLength =8,ErrorMessage ="The {0} must be at {2} and at max {1} character")]
        [DataType(DataType.Password)]
        [Compare("ConfirmPassword",ErrorMessage ="Password does not match")]
        public string Password { get; set; }

        [Required(ErrorMessage = "ConfirmPassword is required")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
