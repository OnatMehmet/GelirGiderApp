using Microsoft.EntityFrameworkCore;
using GelirGiderApp.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Hosting;
using GelirGiderApp.SeedData;

var builder = WebApplication.CreateBuilder(args);

// Veritabaný baðlantýsý
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Identity ayarlarý
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
{
    // Disable all password restrictions
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 3;
    options.Password.RequiredUniqueChars = 0;
})
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Oturum yönetimi
//builder.Services.ConfigureApplicationCookie(options =>
//{
//    options.LoginPath = "/Account/Login"; // Kullanýcý giriþ yapmamýþsa yönlendirilecek sayfa
//    options.AccessDeniedPath = "/Account/AccessDenied";
//});

// HttpContextAccessor ekleyelim
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllersWithViews();


// Controller'lara varsayýlan olarak Authorize ekleyelim
//builder.Services.AddControllersWithViews(options =>
//{
//    var policy = new AuthorizationPolicyBuilder()
//                    .RequireAuthenticatedUser()
//                    .Build();
//    options.Filters.Add(new AuthorizeFilter(policy));
//});


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
    var roleManager = services.GetRequiredService<RoleManager<ApplicationRole>>();
    SeedData.Initialize(services, userManager, roleManager).Wait();
   
}


//using(var scope = app.Services.CreateScope())
//{
//    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();


//    // Roller var mý diye kontrol et
//    if (!context.Roles.Any())
//    {
//        context.Roles.AddRange(new Role
//        {
//            Name = "Admin"
//        }, new Role
//        {
//            Name = "Doctor"
//        }, new Role 
//        { 
//            Name ="User"
//        });

//        context.SaveChanges(true);
//    }

//    // Eðer kullanýcý yoksa admin kullanýcýsýný ekle
//    if (!context.Users.Any())
//    {
//        context.Users.Add(new User
//        {
//            Username ="admin",
//            Password = "admin123",
//            RoleId  = context.Roles.FirstOrDefault(r =>r.Name == "Admin").Id
//        });

//        context.SaveChanges(true);
//    }
//}

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
