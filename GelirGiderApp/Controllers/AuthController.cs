using Microsoft.AspNetCore.Mvc;
using GelirGiderApp.Models;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using GelirGiderApp.Models.Entities;
using System.Net;

namespace GelirGiderApp.Controllers
{
    public class AuthController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginModel login)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == login.Username && u.Password == login.Password);
            if (user == null )// || !VerifyPasswordHash(login.Password, user.Password)
            {
                ModelState.AddModelError("", "Geçersiz kullanıcı adı veya şifre");
                ViewBag.ErrorMessage = "Geçersiz kullanıcı adı veya şifre";

                return View();
            }

            var token = GenerateJwtToken(user);
            // Token'ı cookie'ye ekliyoruz
            Response.Cookies.Append("jwt_token", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,  // Eğer HTTPS kullanıyorsanız, bu güvenlik için eklenebilir
                Expires = DateTime.Now.AddHours(1),  // Token'ın geçerlilik süresi
                SameSite = SameSiteMode.Strict
            });
            return RedirectToAction("Index", "Home"); // Giriş yaptıktan sonra yönlendirecek sayfa
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Username),  // Kullanıcı adı
            new Claim(JwtRegisteredClaimNames.Sub, user.Username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("role", _context.Roles.FirstOrDefault(x=>x.Id == user.RoleId).Name) // Kullanıcının rolünü de ekliyoruz
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );


            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private bool VerifyPasswordHash(string password, string storedHash)
        {
            var passwordHasher = new PasswordHasher<User>();
            return passwordHasher.VerifyHashedPassword(null, storedHash, password) == PasswordVerificationResult.Success;
        }

        // Çıkış işlemi
        public IActionResult Logout()
        {
            // Oturumu kapatıyoruz
            HttpContext.Session.Clear();
            HttpContext.Response.Cookies.Delete("jwt_token");
            //HttpContext.Session.Remove("Username");
            return RedirectToAction("Login", "Auth");
        }
    }
}
