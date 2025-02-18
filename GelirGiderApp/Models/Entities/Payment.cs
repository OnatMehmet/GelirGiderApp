namespace GelirGiderApp.Models.Entities
{
    public class Payment :BaseEntity<Payment>
    {
        public Guid PatientProductId { get; set; }  // Hasta-Ürün ilişkisi
        public PatientProduct PatientProduct { get; set; }  // Hasta-Ürün objesi ile ilişki
        public decimal Amount { get; set; }  // Yapılan ödeme
        public DateTime PaymentDate { get; set; }  // Ödeme tarihi
    }
}
