using GelirGiderApp.Models.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace GelirGiderApp.Models.Configurations
{
    public class ProductTypeConfiguration : IEntityTypeConfiguration<ProductType>
    {
        public void Configure(EntityTypeBuilder<ProductType> builder)
        {
            builder.HasKey(pt => pt.Id);

            builder.Property(pt => pt.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasMany(pt => pt.Products)
                .WithOne(p => p.ProductType)
                .HasForeignKey(p => p.ProductTypeId);

            builder.Property(pt => pt.AnalysisPurchasePrice)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(p => p.AnalysisSalesPrice)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(pt => pt.MonthlyPurchasePrice)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(p => p.MonthlySalesPrice)
                .HasColumnType("decimal(18,2)")
                .IsRequired();
        }
    }
}
