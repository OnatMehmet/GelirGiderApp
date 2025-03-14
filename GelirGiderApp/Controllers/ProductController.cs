using GelirGiderApp.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GelirGiderApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: product
        public async Task<IActionResult> Index()
        {
            var products = _context.Products.Where(x=>x.IsActive)
                           .Include(p => p.ProductType)// Ürün tipi ile birlikte çek
                           .ToList();
            return View(products);
        }

        // GET: product/Create
        [HttpGet]
        public IActionResult  Create()
        {// Ürün tiplerini veritabanından çek
         var productTypes =  _context.ProductTypes.ToList();
            // View'a veri gönder
            ViewBag.ProductTypes = productTypes;
            return View();
        }

        // POST: product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                // Ürün tipi ID boş mu?
                if (product.ProductTypeId == Guid.Empty)
                {
                    ModelState.AddModelError("", "Ürün tipi seçmelisiniz.");
                    ViewBag.ProductTypes = _context.ProductTypes.ToList(); // Dropdown için tekrar yükle
                    return View(product);
                }
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index)); // Ürünler listesine yönlendirme
            }
            return View(product);
        }
        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            ViewBag.ProductTypes = _context.ProductTypes.ToList();
            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var product = _context.Products.Include(p => p.ProductType).FirstOrDefault(p => p.Id == model.Id);
            if (product == null)
            {
                return NotFound();
            }

            // Hasta bilgilerini güncelle
            product.Name = model.Name;
            product.Code = model.Code;
            product.Cost = model.Cost;
            product.SalePrice = model.SalePrice;
            product.ProductTypeId = model.ProductTypeId;
            //product.ProductType.Name = model.ProductType.Name;
            product.Description = model.Description;

            //// Eğer modelde yeni bir ProductType adı girilmişse güncelle
            //if (model.ProductType != null && !string.IsNullOrEmpty(model.ProductType.Name))
            //{
            //    var existingProductType = _context.ProductTypes.Find(model.ProductTypeId);
            //    if (existingProductType != null)
            //    {
            //        existingProductType.Name = model.ProductType.Name;
            //    }
            //}
            _context.SaveChanges();
            TempData["Success"] = "Ürün başarıyla güncellendi!";
            return RedirectToAction("Index");
        }

        // GET: Ürün/Delete/5
        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {

            var urun = await _context.Products.FindAsync(id);
            if (urun == null)
            {
                return Json(new { success = false, message = "Hasta bulunamadı!" });
            }
            urun.IsActive = false;
            _context.Products.Update(urun);
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Ürün başarıyla silindi!" });
        }

        // GET: product/Create
        [HttpGet]
        public IActionResult ProductPricing()
        {// Ürün tiplerini veritabanından çek
            var productTypes = _context.ProductTypes.ToList();
            // View'a veri gönder
            ViewBag.ProductTypes = productTypes;
            return View();
        }

        // POST: product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductPricing(Product product)
        {
            if (ModelState.IsValid)
            {
                // Ürün tipi ID boş mu?
                if (product.ProductTypeId == Guid.Empty)
                {
                    ModelState.AddModelError("", "Ürün tipi seçmelisiniz.");
                    ViewBag.ProductTypes = _context.ProductTypes.ToList(); // Dropdown için tekrar yükle
                    return View(product);
                }
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index)); // Ürünler listesine yönlendirme
            }
            return View(product);
        }

        [HttpGet]
        public async Task<IActionResult> GetProductDetails(Guid productId)
        {
            var product = await _context.Products
                .Include(p => p.ProductType) // Ürün tipi bilgilerini de al
                .FirstOrDefaultAsync(p => p.Id == productId);

            if (product == null)
                return NotFound();

            return Json(new
            {
                MonthlyPurchasePrice = product.ProductType.MonthlyPurchasePrice, // Aylık kullanım fiyatı
                AnalysisPurchasePrice = product.ProductType.AnalysisPurchasePrice // Analiz fiyatı
            });
        }


    }
}

