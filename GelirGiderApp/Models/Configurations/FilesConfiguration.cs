using GelirGiderApp.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace GelirGiderApp.Models.Configurations
{
    public class FilesConfiguration : IEntityTypeConfiguration<Files>
    {
        public void Configure(EntityTypeBuilder<Files> builder)
        {

            builder.HasKey(f => f.Id);
            builder.Property(f => f.FileName)
                .IsRequired();
            builder.Property(f => f.FilePath)
                .IsRequired()
                .HasMaxLength(200);

            builder.HasOne(f => f.Patient)
                .WithMany(p => p.Files)
                .HasForeignKey(f => f.PatientId)
                .OnDelete(DeleteBehavior.Cascade); // Hasta silindiğinde dosyalar da silinsin
        }
    }
}
