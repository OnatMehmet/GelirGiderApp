using GelirGiderApp.Models.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace GelirGiderApp.Models.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(p => p.Code)
                .IsRequired()
                .HasMaxLength(50);

            //builder.Property(p => p.MonthlyPrice)
            //    .HasColumnType("decimal(18,2)")
            //    .IsRequired();

            //builder.Property(p => p.AnalysisPrice)
            //    .HasColumnType("decimal(18,2)")
            //    .IsRequired();

            builder.HasOne(p => p.ProductType)
                .WithMany(pt => pt.Products)
                .HasForeignKey(p => p.ProductTypeId);

            builder.HasIndex(p => p.Code).IsUnique();
            //.HasAnnotation("Index", new IndexAnnotation(new IndexAttribute() { IsUnique = true }));
        }
    }
}