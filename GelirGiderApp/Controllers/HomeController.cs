using GelirGiderApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;

namespace GelirGiderApp.Controllers
{ 
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        public IActionResult Index()
        {
            // Oturumda kullanýcý adý var mý kontrol et
            //var username = User.Identity.Name; //HttpContext.Session.GetString("Username");

            var token = Request.Cookies["jwt_token"];

            if (token != null)
            {
                var username = GetUsernameFromToken(token);
                Guid userRoleId = _context.Users.FirstOrDefault(x => x.Username == username).RoleId ;

                var userRole = _context.Roles.FirstOrDefault(x => x.Id == userRoleId).Name;
                ViewBag.Username = username;
                ViewBag.userRole = userRole;

                ViewBag.TotalPatients = _context.Patients.Where(x => x.IsActive).Count();
                ViewBag.TotalProducts = _context.Products.Where(x => x.IsActive).Count();
                ViewBag.TotalIncome = _context.Payments.Where(x => x.IsActive).Sum(s => s.Amount);
                ViewBag.TotalExpense = _context.Products.Where(x => x.IsActive).Sum(e => e.Cost);
                ViewBag.TotalRemainingPayments = 5;//_context.Sales.Sum(s => s.ProductSalePrice - s.AmountPaid);

            }
            else
            {
                return RedirectToAction("Login", "Auth");
            }

            return View();
        }
        private string GetUsernameFromToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;
            var username = jsonToken?.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value;
            return username;
        }
    

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
