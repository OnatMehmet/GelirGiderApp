using Microsoft.CodeAnalysis;

namespace GelirGiderApp.Models.Entities
{
    public class Patient :BaseEntity<Guid>
    {
        public required string Name { get; set; }
        //public string FullName => FirstName + " " + LastName;
        public string? Email { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty.ToString();
        public DateTime StartDate { get; set; }  // Ürünü kullanmaya başlama tarihi
        public DateTime? EndDate { get; set; }  // Ürün bitiş tarihi (isteğe bağlı)
        public virtual ICollection<Note>? Notes { get; set; }
        public virtual ICollection<Sales>? Sales { get; set; }
        public virtual ICollection<Payment>? Payments { get; set; }
        public ICollection<Files>? Files { get; set; }  // Hastanın Dosyaları
        public ICollection<Diagnosis>? Diagnoses { get; set; }  // Hastanın Tanıları

    }
}
