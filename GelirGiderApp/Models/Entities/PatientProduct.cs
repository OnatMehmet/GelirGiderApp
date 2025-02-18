namespace GelirGiderApp.Models.Entities
{
    public class PatientProduct :BaseEntity<PatientProduct>
    {
        public Guid PatientId { get; set; }  // Hasta ID'si
        public Patient Patient { get; set; }  // Hasta objesi ile ilişki
        public Guid ProductId { get; set; }  // Ürün ID'si
        public Product Product { get; set; }  // Ürün objesi ile ilişki
        public DateTime StartDate { get; set; }  // Ürüne başlama tarihi
        public DateTime? EndDate { get; set; }  // Ürün bitiş tarihi (isteğe bağlı)
        public List<Payment> Payments { get; set; }  // Ödemeler
    }
}
