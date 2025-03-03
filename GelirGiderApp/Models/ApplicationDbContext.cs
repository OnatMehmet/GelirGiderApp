﻿using GelirGiderApp.Models.Entities;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IHttpContextAccessor httpContextAccessor)
    : base(options)
    {

        _httpContextAccessor = httpContextAccessor;
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<ProductType> ProductTypes { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Sales> Sales { get; set; }
    public DbSet<Note> Notes { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Files> Files { get; set; }
    public DbSet<Diagnosis> Diagnoses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Rolleri ekleyelim: Admin ve Doktor
        modelBuilder.Entity<Role>().HasData(
            new Role { Id = Guid.NewGuid(), Name = "Admin" },
            new Role { Id = Guid.NewGuid(), Name = "Doctor" }
        );
        // Dosya ile ilgili konfigürasyonlar
        modelBuilder.Entity<Files>()
            .HasOne(f => f.Patient)
            .WithMany(p => p.Files)
            .HasForeignKey(f => f.PatientId)
            .OnDelete(DeleteBehavior.Cascade);  // Hasta silindiğinde dosyalar da silinsin

        // Tanı ile ilgili konfigürasyonlar
        modelBuilder.Entity<Diagnosis>()
            .HasOne(d => d.Patient)
            .WithMany(p => p.Diagnoses)
            .HasForeignKey(d => d.PatientId)
            .OnDelete(DeleteBehavior.Cascade);  // Hasta silindiğinde tanılar da silinsin

        //Code Alanı benzersiz olsun
        modelBuilder.Entity<Product>()
            .HasIndex(p => p.Code).IsUnique();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries()
     .Where(e => e.Entity is BaseEntity<object> &&
                 (e.State == EntityState.Added || e.State == EntityState.Modified))
     .ToList();

        var currentUser = _httpContextAccessor.HttpContext?.User?.Identity?.Name ?? "System"; // Mevcut kullanıcı adı (giriş yapmış kullanıcı)

        foreach (var entry in entries)
        {
            if (entry.Entity is BaseEntity<object> entity)
            {
                if (entry.State == EntityState.Added)
                {
                    entity.CreatedBy = currentUser;
                    entity.CreatedDate = DateTime.Now;
                }

                if (entry.State == EntityState.Modified)
                {
                    entity.UpdatedBy = currentUser;
                    entity.UpdatedDate = DateTime.Now;
                }
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
}
