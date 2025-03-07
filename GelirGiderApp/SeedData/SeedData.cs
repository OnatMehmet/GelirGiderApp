using GelirGiderApp.Models.Entities;
using Microsoft.AspNetCore.Identity;
using static System.Formats.Asn1.AsnWriter;

namespace GelirGiderApp.SeedData
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            var roleNames = new[] { "Admin", "User", "Manager","Doctor" };
            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    var role = new ApplicationRole { Name = roleName };
                    await roleManager.CreateAsync(role);
                }
            }

            var user = await userManager.FindByEmailAsync("admin@gmail.com");
            if (user == null)
            {
                user = new ApplicationUser
                {
                    FirstName = "Admin",
                    LastName = "User",
                    UserName = "admin",
                    Email = "admin@gmail.com"
                };

                var result = await userManager.CreateAsync(user, "Admin123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                }
            }
        }
    }

}
