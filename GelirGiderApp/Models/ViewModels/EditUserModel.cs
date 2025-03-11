using System.ComponentModel.DataAnnotations;

namespace GelirGiderApp.Models.ViewModels
{
    public class EditUserModel
    {
        public string Id { get; set; }  // Kullanıcı ID'si
        public string UserName { get; set; }  // Kullanıcı Adı
        public string FirstName { get; set; }  // Kullanıcı Adı
        public string LastName { get; set; }  // Kullanıcı Adı
        public string Email { get; set; }  // Email
                                           //[Required(ErrorMessage = "Telefon numarası zorunludur.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Telefon numarası 10 haneli olmalıdır.")]
        public string PhoneNumber { get; set; }  // Telefon Numarası

        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Şifre en az 6 karakter olmalıdır.")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Şifreler eşleşmiyor.")]
        public string ConfirmPassword { get; set; }
        public List<string> Roles { get; set; } = new();  // Tüm Roller
        public string SelectedRole { get; set; }  // Seçili Rol
    }

}
