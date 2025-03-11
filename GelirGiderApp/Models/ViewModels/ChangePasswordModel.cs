using System.ComponentModel.DataAnnotations;

namespace GelirGiderApp.Models.ViewModels
{
    public class ChangePasswordModel
    {
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Yeni şifre en az 6 karakter olmalıdır.")]
        public string NewPassword { get; set; }

        [Required]
        [Compare("NewPassword", ErrorMessage = "Yeni şifreler eşleşmiyor.")]
        public string ConfirmPassword { get; set; }
    }

}
