namespace GelirGiderApp.Models.Entities
{
    public class Payment: BaseEntity<Payment>
    {
        public decimal? Amount { get; set; }
        public DateTime PaymentDate { get; set; }

        public Guid PatientId { get; set; }
        public Patient Patient { get; set; }
        public Guid SalesId { get; set; }
        public Sales Sales { get; set; }


    }
}
