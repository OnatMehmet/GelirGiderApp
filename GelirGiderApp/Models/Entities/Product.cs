namespace GelirGiderApp.Models.Entities
{
    public class Product :BaseEntity<Product>
    {
        public required string Name { get; set; }  // Ürün adı
        public decimal Cost { get; set; }  // Ürün maliyeti
        public decimal SalePrice { get; set; }  // Ürün satış fiyatı
        public Guid ProductTypeId { get; set; }  // Ürün tipi ID'si (Foreign Key)
        public ProductType? ProductType { get; set; }  // Ürün tipi ile ilişki
    }
}
