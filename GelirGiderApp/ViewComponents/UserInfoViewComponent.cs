using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using GelirGiderApp.Models.Entities;

public class UserInfoViewComponent : ViewComponent
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UserInfoViewComponent(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var user = await _userManager.GetUserAsync(UserClaimsPrincipal);
        return View(user);
    }
}
