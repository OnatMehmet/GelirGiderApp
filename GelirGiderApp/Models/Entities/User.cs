using System.Data;

namespace GelirGiderApp.Models.Entities
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty ;
        public string Email { get; set; } = string.Empty;
        public required string Username { get; set; }  // Kullanıcı adı
        public required string Password { get; set; }  // Şifre hash'i
        public Guid RoleId { get; set; }  // Kullanıcının rolü (Admin, Doktor)
        public Role? Role { get; set; }  // Role navigasyon özelliği
    }
}
