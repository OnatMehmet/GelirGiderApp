using GelirGiderApp.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace GelirGiderApp.Controllers
{
    public class ProductTypeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductTypeController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var productTypes = await _context.ProductTypes.Where(x => x.IsActive).ToListAsync();
            return View(productTypes);
        }
        // GET: product/Create
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductType model)
        {
            if (string.IsNullOrEmpty(model.Name))
            {
                return Json(new { success = false });
            }

            // Yeni ürün tipi oluştur
            var newProductType = new ProductType
            {
                Name = model.Name,
                MonthlyPurchasePrice = model.MonthlyPurchasePrice,
                AnalysisPurchasePrice = model.AnalysisPurchasePrice,
                Description = model.Description
            };

            // Veritabanına kaydet
            _context.ProductTypes.Add(newProductType);
            await _context.SaveChangesAsync();

            //  if (modal)
            //  {
            //// Başarılı olursa ID'yi döndür
            //  return Json(new { success = true, id = newProductType.Id });
            //  }
            //  else
            //  {

            //  }
            TempData["SuccessMessage"] = "Ürün Tipi başarıyla eklendi!";
            return RedirectToAction(nameof(Index));




        }

        // Ürün tipini düzenleme sayfasına yönlendir
        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var productType = _context.ProductTypes.Find(id);
            if (productType == null)
            {
                return NotFound();
            }
            return View(productType);
        }

        [HttpPost]
        public IActionResult Edit(ProductType model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var productType = _context.ProductTypes.FirstOrDefault(p => p.Id == model.Id);
            if (productType == null)
            {
                return NotFound();
            }

            // Ürünj Tipi bilgilerini güncelle
            productType.Name = model.Name;
            productType.MonthlyPurchasePrice = model.MonthlyPurchasePrice;
            productType.AnalysisPurchasePrice = model.AnalysisPurchasePrice;
            productType.Description = model.Description;

            _context.SaveChanges();
            TempData["Success"] = "Ürün Tipi başarıyla güncellendi!";
            return RedirectToAction("Index");
        }
        // Ürün tipi silme
        [HttpPost]
        public IActionResult Delete(Guid id)
        {
            var productType = _context.ProductTypes.Find(id);
            if (productType == null)
            {
                return Json(new { success = false });
            }

            _context.ProductTypes.Remove(productType);
            _context.SaveChanges();
            return Json(new { success = true });
        }


    }
}
