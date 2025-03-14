using GelirGiderApp.Models.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace GelirGiderApp.Models.Configurations
{
    public class PatientProductConfiguration : IEntityTypeConfiguration<PatientProduct>
    {
        public void Configure(EntityTypeBuilder<PatientProduct> builder)
        {
            builder.HasKey(pp => pp.Id);

            builder.Property(pp => pp.UsageStage)
                .IsRequired()
                .HasMaxLength(50);

            // Hasta silindiğinde, ilişkili hasta-ürün kayıtları da silinecek
            builder.HasOne(pp => pp.Patient)
                .WithMany(p => p.PatientProducts)
                .HasForeignKey(pp => pp.PatientId)
                .OnDelete(DeleteBehavior.Cascade); // ✅ Hasta silindiğinde ilişkili verileri de sil

            // Ürün ilişkisini NO ACTION yapıyoruz, çakışma olmaması için
            builder.HasOne(pp => pp.Product)
                .WithMany(p => p.PatientProducts)
                .HasForeignKey(pp => pp.ProductId)
                .OnDelete(DeleteBehavior.NoAction); // ❌ Çakışmayı önlemek için

            // Ürün Tipi ilişkisini NO ACTION yapıyoruz, çakışma olmaması için
            builder.HasOne(pp => pp.ProductType)
                .WithMany()
                .HasForeignKey(pp => pp.ProductTypeId)
                .OnDelete(DeleteBehavior.NoAction); // ❌ Çakışmayı önlemek için
        }
    }
}
