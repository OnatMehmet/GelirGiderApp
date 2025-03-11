using GelirGiderApp.Models.Entities;
using GelirGiderApp.Models.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[Route("Account")]
public class AccountController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }
    [HttpGet("Login")]
    public IActionResult Login(string returnUrl = "/")
    {
        return View(); // Login.cshtml view'ini döndür
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login( LoginModel model)
    {

        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var roles = await _userManager.GetRolesAsync(user);
                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email)
            };

                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role)); // Rolleri ekle
                }

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true
                };

                await HttpContext.SignInAsync(IdentityConstants.ApplicationScheme,
                    new ClaimsPrincipal(claimsIdentity), authProperties);


                return RedirectToAction("Index", "Home");
            }
        }

        ModelState.AddModelError("", "Geçersiz giriş denemesi.");
        return View(model);
    }


    // Register GET
    [HttpGet("Register")]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register(RegisterModel model)
    {
        var user = new ApplicationUser { 
            UserName = model.UserName, 
            Email = model.Email, 
            FirstName = model.FirstName, 
            LastName = model.LastName, 
            PhoneNumber = model.PhoneNumber
            };
        var result = await _userManager.CreateAsync(user, model.Password);
        if (result.Succeeded)
        {
            await _signInManager.SignInAsync(user, isPersistent: false);
            //return Ok(new { message = "Kayıt başarılı!" });
            return RedirectToAction("Login");
        }
        return View();
        //return BadRequest(new { message = "Kayıt başarısız!" });
    }
    [HttpGet("Logout")]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Login", "Account");
    }

    //[HttpPost("Logout")]
    //public async Task<IActionResult> Logout()
    //{
    //    await _signInManager.SignOutAsync();
    //    //return Ok(new { message = "Çıkış yapıldı!" });
    //    return RedirectToAction("Login");
    //}

    [HttpGet("AccessDenied")]
    public IActionResult AccessDenied()
    {
        return Unauthorized(new { message = "Yetkisiz erişim!" });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ChangePassword(ChangePasswordModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            // Şifreyi değiştiriyoruz, eski şifre kontrolüne gerek yok.
            var result = await _userManager.RemovePasswordAsync(user);  // Eski şifreyi kaldırıyoruz.
            if (result.Succeeded)
            {
                var addPasswordResult = await _userManager.AddPasswordAsync(user, model.NewPassword);  // Yeni şifreyi ekliyoruz.
                if (addPasswordResult.Succeeded)
                {
                    return RedirectToAction("Profile");
                }
                foreach (var error in addPasswordResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
        }
        return View(model);
    }

}