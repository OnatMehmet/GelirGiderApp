﻿namespace GelirGiderApp.Models.Entities
{
    public class Product :BaseEntity
    {
        public required string Name { get; set; }  // Ürün adı
    
        public required string Code { get; set; }  // Ürün Kodu
        public decimal Cost { get; set; }  // Ürün maliyeti
        public decimal SalePrice { get; set; }  // Ürün satış fiyatı
        public Guid ProductTypeId { get; set; }  // Ürün tipi ID'si (Foreign Key)
        public ProductType? ProductType { get; set; }  // Ürün tipi ile ilişki
        public ICollection<PatientProduct> PatientProducts { get; set; }
    }
}
