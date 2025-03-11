using System.ComponentModel.DataAnnotations;

namespace GelirGiderApp.Models.ViewModels
{
    public class LoginModel
    {
        [Required(ErrorMessage ="Lütfen Kullanıcı Adını Giriniz")]
        public required string UserName { get; set; }
        [Required(ErrorMessage = "Lütfen Şifrenizi Giriniz")]
        public required string Password { get; set; }
    }
}
