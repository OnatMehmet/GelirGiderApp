using GelirGiderApp.Models;
using GelirGiderApp.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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

        var user = await _userManager.FindByNameAsync(model.UserName);
        if (user != null)
        {
            var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
            if (result.Succeeded)
            {
                //return Ok(new { message = "Giriş başarılı!" });
                return RedirectToAction("Index", "Home");
            }
        }
        TempData["ErrorMessage"] = "Giriş Bilgileri Geçersiz!!";
        return View(); //Unauthorized(new { message = "Geçersiz giriş bilgileri!" });
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
    [HttpPost("Logout")]
    [ValidateAntiForgeryToken]
   // [HttpGet("Logout")]
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
}