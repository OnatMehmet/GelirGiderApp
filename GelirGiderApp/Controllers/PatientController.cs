
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GelirGiderApp.Models.Entities;

namespace GelirGiderApp.Controllers
{
    public class PatientController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PatientController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Hasta
        public async Task<IActionResult> Index()
        {
            var hastalar = await _context.Patients.Where(x=>x.IsActive).ToListAsync();
            return View(hastalar);
        }

        // GET: Hasta/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Hasta/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Email,PhoneNumber,Address,StartDate,Description")] Patient hasta)
        {
      
            if (ModelState.IsValid)
            {   // Telefon numarasını kontrol et
                //var existingPatient = _context.Patients.FirstOrDefault(p => p.PhoneNumber == hasta.PhoneNumber);

                //if (existingPatient != null)
                //{
                //    // Eğer telefon numarası zaten kayıtlıysa, hata mesajı döndür
                //    ModelState.AddModelError("PhoneNumber", "Bu telefon numarası zaten kayıtlı!");
                //    return View(hasta);
                //}
                _context.Add(hasta);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Hasta başarıyla eklendi!";
                return RedirectToAction(nameof(Index));
            }
            return View(hasta);

        }
        [HttpGet]
        public IActionResult IsPhoneExist(string phoneNumber)
        {
            bool isExist = _context.Patients.Any(p => p.PhoneNumber == phoneNumber);
            return Json(new { exists = isExist});
        }
        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var patient = _context.Patients.FirstOrDefault(p => p.Id == id);
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        [HttpPost]
        public IActionResult Edit(Patient model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var patient = _context.Patients.FirstOrDefault(p => p.Id == model.Id);
            if (patient == null)
            {
                return NotFound();
            }

            // Hasta bilgilerini güncelle
            patient.Name = model.Name;
            patient.PhoneNumber = model.PhoneNumber;
            patient.Email = model.Email;
            patient.Address = model.Address;
            patient.StartDate = model.StartDate;
            patient.Description = model.Description;

            _context.SaveChanges();

            TempData["Success"] = "Hasta başarıyla güncellendi!";
            return RedirectToAction("Index");
        }

        // GET: Hasta/Delete/5
        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {

            var hasta = await _context.Patients.FindAsync(id);
            if (hasta == null)
            {
                return Json(new { success = false, message = "Hasta bulunamadı!" });
            }
            hasta.IsActive = false;
            _context.Patients.Update(hasta);
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Hasta başarıyla silindi!" });
        }

        private bool HastaExists(Guid id)
        {
            return _context.Patients.Any(e => e.Id == id);
        }
    }
}

