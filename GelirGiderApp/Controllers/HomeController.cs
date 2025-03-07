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
            var userName = User.Identity.Name; // Giriþ yapan kullanýcýnýn adýný al
            //return Ok(new { message = $"Hoþgeldin {userName}" });
            if (string.IsNullOrEmpty(userName))
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                //Guid userRoleId = _context.Users.FirstOrDefault(x => x.Username == userName).RoleId;

                //var userRole = _context.Roles.FirstOrDefault(x => x.Id == userRoleId).Name;
                ViewBag.Username = userName;
                ViewBag.userRole = "Admin";//userRole;

                ViewBag.TotalPatients = _context.Patients.Where(x => x.IsActive).Count();
                ViewBag.TotalProducts = _context.Products.Where(x => x.IsActive).Count();
                ViewBag.TotalIncome = _context.Sales.Where(x => x.IsActive).Sum(s => s.PaymentAmount);
                ViewBag.TotalExpense = _context.Products.Where(x => x.IsActive).Sum(e => e.Cost);//maliyet
                ViewBag.TotalSales = _context.Sales.Where(x => x.IsActive).Count();
                ViewBag.TotalRemainingPayments = _context.Sales.Where(x => x.IsActive).Sum(s =>s.Price - s.PaymentAmount);
                return View();
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
