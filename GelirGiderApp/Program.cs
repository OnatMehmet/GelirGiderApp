using Microsoft.EntityFrameworkCore;
using GelirGiderApp.Models.Entities;

var builder = WebApplication.CreateBuilder(args);


// Session i�in gerekli servisleri ekliyoruz
builder.Services.AddDistributedMemoryCache();  // Memory cache kullan�m�
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Session s�resi
    options.Cookie.HttpOnly = true; // G�venlik amac�yla sadece HTTP �zerinden eri�ilebilir
    options.Cookie.IsEssential = true; // Cookie'nin gerekli oldu�unu belirtiyoruz
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


    // Roller var m� diye kontrol et
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

    // E�er kullan�c� yoksa admin kullan�c�s�n� ekle
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

// Session middleware'ini kullanmak i�in bu sat�r� ekliyoruz
app.UseSession();  // Session middleware'ini kullanmaya ba�l�yoruz

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

app.UseAuthentication();  // JWT do�rulama
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
