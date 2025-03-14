using GelirGiderApp.Models.Configurations;
using GelirGiderApp.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
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
    public DbSet<UserPermission> UserPermissions { get; set; }
    public DbSet<Page> Pages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.ApplyConfiguration(new ProductTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ProductConfiguration());
        modelBuilder.ApplyConfiguration(new PatientProductConfiguration());

        base.OnModelCreating(modelBuilder);


        //modelBuilder.Entity<ApplicationUser>(entity =>
        //{
        //    entity.Property(e => e.FirstName).HasColumnName("FirstName");
        //    entity.Property(e => e.LastName).HasColumnName("LastName");
        //});


        // Rolleri ekleyelim: Admin
        modelBuilder.Entity<Role>().HasData(
            new Role { Id = Guid.NewGuid(), Name = "Admin" }
        );
    }
    public override int SaveChanges()
    {
        SetAuditFields();
        return base.SaveChanges();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        SetAuditFields();
        return await base.SaveChangesAsync(cancellationToken);
    }

    private void SetAuditFields()
    {
        var entries = ChangeTracker.Entries()
            .Where(e => e.Entity is BaseEntity && (e.State == EntityState.Added || e.State == EntityState.Modified));

        string currentUser = _httpContextAccessor.HttpContext?.User?.Identity?.Name ?? "System"; // Kullanıcı adı

        foreach (var entry in entries)
        {
            var entity = (BaseEntity)entry.Entity;

            if (entry.State == EntityState.Added)
            {
                entity.CreatedDate = DateTime.UtcNow;
                entity.CreatedBy ??= currentUser; // Eğer CreatedBy boşsa, sadece ilk seferde ekle
            }

            entity.UpdatedDate = DateTime.UtcNow;
            entity.UpdatedBy = currentUser;
        }
    }
}
