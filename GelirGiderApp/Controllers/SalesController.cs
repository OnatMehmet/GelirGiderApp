using GelirGiderApp.Models;
using GelirGiderApp.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace GelirGiderApp.Controllers
{
    public class SalesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SalesController(ApplicationDbContext context)
        {
            _context = context;
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
        public IActionResult Create(SalesViewModel model)
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
