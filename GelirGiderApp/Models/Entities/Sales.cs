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

        //Kullanım Aşaması

        public ProductUsageStage UsageStage { get; set; } = ProductUsageStage.Gaita;
    }
    public enum ProductUsageStage
    {
        [Description("Gaita Analizi")]
        Gaita = 0,
        [Description("1.Ay")]
        BirinciAy = 1,
        [Description("2.Ay")]
        IkinciAy = 2,
        [Description("3.Ay")]
        UcuncuAy = 3,
        [Description("4.Ay")]
        DorduncuAy = 4,
        [Description("5.Ay")]
        BesinciAy = 5,
        [Description("6.Ay")]
        Tamamlandi = 6
    }

}
