using GelirGiderApp.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            var sales = await _context.Sales
                .Where(x=>x.IsActive)
                .Include(p => p.Patient)
                .Include(s => s.Product)
                .ToListAsync();
            return View(sales);
        }

        [HttpGet]
        public JsonResult HastaAra(string search)
        {
            var patients = _context.Patients
                                  .Where(p => p.Name.Contains(search))
                                  .Select(p => new { p.Id, p.Name })
                                  .ToList();
            return Json(patients);
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

        [HttpPost]
        public async Task<IActionResult> Create(Sales sales)
        {
            if (ModelState.IsValid)
            {
                var hasta = await _context.Patients.FirstOrDefaultAsync(h => h.Id == sales.PatientId);
                if (hasta == null)
                {
                    ModelState.AddModelError("PatientId", "Hasta zorunlu");
                    return View(sales);
                }

                sales.CreatedDate = DateTime.UtcNow;
                sales.RemainingAmount = sales.Price - sales.PaymentAmount;
                _context.Sales.Add(sales);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");//RedirectToAction("SatisDetay", new { id = satis.Id });
            }
            return View(sales);
        }

        public IActionResult Edit(Guid id)
        {
            var sale = _context.Sales
          .Include(s => s.Patient)
          .Include(s => s.Product)
          .FirstOrDefault(s => s.Id == id);

            if (sale == null)
            {
                return NotFound();
            }

            ViewBag.Patients = _context.Patients.ToList();
            ViewBag.Products = _context.Products.ToList();

            return View(sale);
        }

        [HttpPost]
        public IActionResult Edit(Sales model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var sales = _context.Sales.Include(p => p.Patient).FirstOrDefault(p => p.Id == model.Id);
            if (sales != null)
            {   // Satış bilgilerini güncelle
                sales.PatientId = model.PatientId;
                sales.ProductId = model.ProductId;
                sales.Price = model.Price;
                sales.Description = model.Description;
                _context.SaveChanges();
                TempData["Success"] = "Satış İşlemi başarıyla güncellendi!";
                return RedirectToAction("Index");
            }

            ViewBag.Patients = _context.Patients.ToList();
            ViewBag.Products = _context.Products.ToList();
            return View(model);

        }

        // GET: Ürün/Delete/5
        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {

            var satis = await _context.Sales.FindAsync(id);
            if (satis == null)
            {
                return Json(new { success = false, message = "Satış bulunamadı!" });
            }
            satis.IsActive = false;
            _context.Sales.Update(satis);
            await _context.SaveChangesAsync();
            return Json(new { success = true, message = "Satış işlemi başarıyla silindi!" });
        }

        [HttpGet]
        public JsonResult GetPrice(Guid id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            return Json(product?.SalePrice ?? 0);
        }
    }

}
