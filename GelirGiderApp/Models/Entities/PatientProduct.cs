namespace GelirGiderApp.Models.Entities
{
    public class PatientProduct :BaseEntity
    {
        public Guid PatientId { get; set; }  // Hasta ID
        public Guid ProductId { get; set; }  // Ürün ID
        public Guid ProductTypeId { get; set; } // Ürün Tipi ID

        public string UsageStage { get; set; }  // Kullanım Aşaması (Gaita Analizi, 1 Aylık, 2 Aylık vb.)

        // Navigasyon Özellikleri
        public Patient Patient { get; set; }
        public Product Product { get; set; }
        public ProductType ProductType { get; set; }
    }


}
