using Microsoft.EntityFrameworkCore;
using GelirGiderApp.Models.Entities;

var builder = WebApplication.CreateBuilder(args);


// Session için gerekli servisleri ekliyoruz
builder.Services.AddDistributedMemoryCache();  // Memory cache kullanýmý
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Session süresi
    options.Cookie.HttpOnly = true; // Güvenlik amacýyla sadece HTTP üzerinden eriþilebilir
    options.Cookie.IsEssential = true; // Cookie'nin gerekli olduðunu belirtiyoruz
});

// IHttpContextAccessor servisinin eklenmesi
builder.Services.AddHttpContextAccessor();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

using(var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();


    // Roller var mý diye kontrol et
    if (!context.Roles.Any())
    {
        context.Roles.AddRange(new Role
        {
            Name = "Admin"
        }, new Role
        {
            Name = "Doctor"
        }, new Role 
        { 
            Name ="User"
        });

        context.SaveChanges(true);
    }

    // Eðer kullanýcý yoksa admin kullanýcýsýný ekle
    if (!context.Users.Any())
    {
        context.Users.Add(new User
        {
            Username ="admin",
            Password = "admin123",
            RoleId  = context.Roles.FirstOrDefault(r =>r.Name == "Admin").Id
        });

        context.SaveChanges(true);
    }
}

// Session middleware'ini kullanmak için bu satýrý ekliyoruz
app.UseSession();  // Session middleware'ini kullanmaya baþlýyoruz

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();  // JWT doðrulama
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
