using GelirGiderApp.Models;
using GelirGiderApp.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;

namespace GelirGiderApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var userName = User.Identity.Name; // Giri� yapan kullan�c�n�n ad�n� al
            //return Ok(new { message = $"Ho�geldin {userName}" });
            if (string.IsNullOrEmpty(userName))
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
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
