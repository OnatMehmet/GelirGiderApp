using GelirGiderApp.Models;
using GelirGiderApp.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.Composition;

namespace GelirGiderApp.Controllers
{
    public class SalesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SalesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: sales
        public async Task<IActionResult> Index()
        {
            var sales = await _context.Sales.ToListAsync();
            return View(sales);
        }

        [HttpGet]
        public async Task<IActionResult> HastaAra(string term)
        {
            var hastalar = await _context.Patients
                .Where(h => h.Name.Contains(term))
                .Select(h => h.Name)
                .Take(10)
                .ToListAsync();
            return Json(hastalar);
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,PaymentAmount,Price,RemainingAmount,PatientId,ProductId")] Sales sales)
        {

            if (ModelState.IsValid)
            {
                var hasta = await _context.Patients.FirstOrDefaultAsync(h => h.Id == sales.PatientId);

                if (hasta == null)
                {
                    ModelState.AddModelError("PatientId", "Hasta zorunlu");
                    return View(sales);
                }
                //if (hasta == null)
                //{
                //    hasta = new Patient { Name = hasta.Name };
                //    _context.Patients.Add(hasta);
                //    await _context.SaveChangesAsync();
                //}

                var satis = new Sales
                {
                    PatientId = sales.Id,
                    ProductId = sales.ProductId,
                    Price = sales.Price,
                    PaymentAmount = sales.PaymentAmount,
                    RemainingAmount = sales.Price - sales.PaymentAmount
                };

                _context.Sales.Add(satis);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");//RedirectToAction("SatisDetay", new { id = satis.Id });
            }
            return View(sales);
        }

        // Yeni satış işlemi için view
        public IActionResult Create()
        {
            var patients = _context.Patients.ToList();  // Tüm hastalar
            var products = _context.Products.ToList();  // Tüm ürünler
            var productTypes = _context.ProductTypes.ToList();  // Ürün tipleri

            // Gerekli bilgileri view'a gönderiyoruz
            ViewBag.Patients = patients;
            ViewBag.Products = products;
            ViewBag.ProductTypes = productTypes;

            return View();
        }



        // Satış işlemini kaydetme
        [HttpPost]
        public IActionResult Create1(SalesViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Yeni hasta kaydı
                var patient = new Patient
                {
                    Name = model.PatientName,
                    StartDate = DateTime.Now,
                };
                _context.Patients.Add(patient);
                _context.SaveChanges();

                // Ürün bilgisi ve ödeme kaydı
                var patientProduct = new PatientProduct
                {
                    PatientId = patient.Id,
                    ProductId = model.ProductId,
                    StartDate = DateTime.Now
                };
                _context.PatientProducts.Add(patientProduct);
                _context.SaveChanges();

                // Ödeme kaydı
                var payment = new Payment
                {
                    PatientProductId = patientProduct.Id,
                    Amount = model.PaymentAmount,
                    PaymentDate = DateTime.Now
                };
                _context.Payments.Add(payment);
                _context.SaveChanges();

                return RedirectToAction("Index", "Home");  // Satış tamamlandıktan sonra yönlendirme
            }

            return View(model);  // Hata durumunda formu yeniden göster
        }
    }

}
