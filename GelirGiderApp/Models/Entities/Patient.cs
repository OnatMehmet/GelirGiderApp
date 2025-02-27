namespace GelirGiderApp.Models.Entities
{
    public class Patient :BaseEntity<Patient>
    {
        public required string Name { get; set; }
        //public string FullName => FirstName + " " + LastName;
        public string? Email { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty.ToString();
        public DateTime StartDate { get; set; }  // Ürünü kullanmaya başlama tarihi
        public ICollection<PatientProduct>? PatientProducts { get; set; }  // Kullanılan ürünler
        public virtual ICollection<Note>? Notes { get; set; }

    }
}
