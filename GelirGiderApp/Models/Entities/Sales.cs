using System.ComponentModel;

namespace GelirGiderApp.Models.Entities
{
    public class Sales :BaseEntity
    {

        public decimal PaymentAmount { get; set; }  // Ödeme miktarı
        public decimal Price { get; set; }  // Ödeme miktarı
        public decimal RemainingAmount { get; set; }  // kalan miktar
        public Guid PatientId { get; set; }

        public Guid ProductId { get; set; }

        public Patient? Patient { get; set; }
        public Product?  Product { get; set; }
        public int Month { get; set; }

        //Kullanım Aşaması
        public ProductUsageStage UsageStage { get; set; } = ProductUsageStage.Gaita;
    }
    public enum ProductUsageStage
    {
        [Description("Gaita Analizi")]
        Gaita = 0,
        [Description("1 Aylık")]
        BirinciAy = 1,
        [Description("2 Aylık")]
        IkinciAy = 2,
        [Description("3 Aylık")]
        UcuncuAy = 3,
        [Description("4 Aylık")]
        DorduncuAy = 4,
        [Description("5 Aylık")]
        BesinciAy = 5,
        [Description("6 Aylık")]
        AltinciAy = 6,
        [Description("Tamamlandı")]
        Tamamlandi = 7
    }

}
