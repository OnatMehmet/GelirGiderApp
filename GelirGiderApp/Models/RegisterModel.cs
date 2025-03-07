namespace GelirGiderApp.Models
{
    public class RegisterModel
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string FullName { get; set; }
        public required string UserName { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public required string Password { get; set; }
        public required string ConfirmPassword { get; set; }
    }
}
