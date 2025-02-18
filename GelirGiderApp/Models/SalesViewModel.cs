namespace GelirGiderApp.Models
{
    public class SalesViewModel
    {
        public  required string PatientName { get; set; }  // Hasta adı
        public Guid ProductId { get; set; }  // Ürün ID'si
        public decimal PaymentAmount { get; set; }  // Ödeme miktarı
    }
}
