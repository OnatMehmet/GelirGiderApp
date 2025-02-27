namespace GelirGiderApp.Models.Entities
{
    public class Sales :BaseEntity<Sales>
    {

        public decimal PaymentAmount { get; set; }  // Ödeme miktarı
        public decimal Price { get; set; }  // Ödeme miktarı
        public decimal RemainingAmount { get; set; }  // Ödeme miktarı
        public Guid PatientId { get; set; }

        public Guid ProductId { get; set; }

        public Patient? Patient { get; set; }
        public Product?  Product { get; set; }
    }
}
