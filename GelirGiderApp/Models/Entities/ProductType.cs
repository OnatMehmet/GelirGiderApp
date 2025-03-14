namespace GelirGiderApp.Models.Entities
{
    public class ProductType : BaseEntity
    {
        public required string Name { get; set; }

        public decimal MonthlyPurchasePrice { get; set; } = decimal.Zero;
        public decimal AnalysisPurchasePrice { get; set; } = decimal.Zero;
        public decimal MonthlySalesPrice { get; set; } = decimal.Zero;
        public decimal AnalysisSalesPrice { get; set; } = decimal.Zero;


        // Navigasyon Özelliği
        public ICollection<Product> Products { get; set; }
    }
}
