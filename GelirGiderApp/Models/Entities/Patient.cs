using Microsoft.CodeAnalysis;
using System.ComponentModel.DataAnnotations;

namespace GelirGiderApp.Models.Entities
{
    public class Patient :BaseEntity
    {
        [Required(ErrorMessage = "İsim alanı zorunludur.")]
        [MinLength(3, ErrorMessage = "İsim en az 3 karakter olmalıdır.")]
        public required string Name { get; set; }
        //public string FullName => FirstName + " " + LastName;

        [EmailAddress(ErrorMessage = "Geçerli bir email adresi giriniz")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Telefon numarası gereklidir.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Telefon numarası 10 haneli olmalıdır.")]
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty.ToString();
        public DateTime StartDate { get; set; }  // Ürünü kullanmaya başlama tarihi
        public DateTime? EndDate { get; set; }  // Ürün bitiş tarihi (isteğe bağlı)
        public virtual ICollection<Note>? Notes { get; set; }
        public virtual ICollection<Sales>? Sales { get; set; }
        public virtual ICollection<Payment>? Payments { get; set; }
        public ICollection<Files>? Files { get; set; }  // Hastanın Dosyaları
        public ICollection<Diagnosis>? Diagnoses { get; set; }  // Hastanın Tanıları
        public ICollection<PatientProduct> PatientProducts { get; set; } //Hasta Ürünleri

    }
}
