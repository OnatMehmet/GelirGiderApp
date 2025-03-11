﻿namespace GelirGiderApp.Models.ViewModels
{
    public class UserProfileViewModel
    {
        public Guid Id { get; set; } // Kullanıcı ID'si
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Role { get; set; }
        public string? NewPassword { get; set; }
        public string? ConfirmPassword { get; set; }
    }
}
