
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

        #region Hasta CRUD işlemleri
        // GET: Hasta
        public async Task<IActionResult> Index()
        {
            var hastalar = await _context.Patients.Where(x => x.IsActive).ToListAsync();
            return View(hastalar);
        }

        // GET: Hasta/Create
        public IActionResult Create()
        {
            return View();
        }
        // POST: Hasta/Create
        [HttpPost]
        public async Task<IActionResult> Create(Patient model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                              .Select(e => e.ErrorMessage)
                                              .ToList();
                return Json(new { success = false, message = string.Join(" ", errors) });
            }

            // Telefon kontrolü
            if (_context.Patients.Any(p => p.PhoneNumber == model.PhoneNumber))
            {
                return Json(new { success = false, message = "Bu telefon numarası zaten kayıtlı!" });
            }

            //// Email kontrolü
            //if (_context.Patients.Any(p => p.Email == model.Email))
            //{
            //    return Json(new { success = false, message = "Bu email adresi zaten kayıtlı!" });
            //}

            await _context.Patients.AddAsync(model);
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Hasta başarıyla eklendi!" });
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
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                              .Select(e => e.ErrorMessage)
                                              .ToList();
                return Json(new { success = false, message = string.Join(" ", errors) });
            }

            var patient = _context.Patients.Find(model.Id);
            if (patient == null)
            {
                return Json(new { success = false, message = "Hasta bulunamadı!" });
            }

            // Telefon numarasını kontrol et (aynı ID hariç)
            if (_context.Patients.Any(p => p.PhoneNumber == model.PhoneNumber && p.Id != model.Id))
            {
                return Json(new { success = false, message = "Bu telefon numarası başka bir hastaya ait!" });
            }

            // Email kontrolü (aynı ID hariç)
            //if (_context.Patients.Any(p => p.Email == model.Email && p.Id != model.Id))
            //{
            //    return Json(new { success = false, message = "Bu email başka bir hastaya ait!" });
            //}

            // Güncelleme işlemi
            patient.Name = model.Name;
            patient.PhoneNumber = model.PhoneNumber;
            patient.Email = model.Email;
            patient.Address = model.Address;
            patient.Description = model.Description;
            patient.StartDate = model.StartDate;

            _context.Patients.Update(patient);
            _context.SaveChanges();

            return Json(new { success = true, message = "Hasta başarıyla güncellendi!" });
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
            hasta.IsDeleted = true;
            _context.Patients.Update(hasta);
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Hasta başarıyla silindi!" });
        }

        #endregion

        #region Hasta Profili
        public IActionResult Detail(Guid id)
        {
            var patient = _context.Patients.Where(p => p.IsActive)
                .Include(p => p.Notes).Where(n => n.IsActive) // Hastanın notlarını getir
                .Include(p => p.Payments).Where(pys => pys.IsActive) // Hastanın ödemelerini getir
                .Include(p => p.Diagnoses.Where(d => d.IsActive))
                .Include(p => p.Files.Where(f => f.IsActive))
                .Include(p => p.Sales.Where(s => s.IsActive))// Hastanın satışlarını getir
                .ThenInclude(p => p.Product)
                .ThenInclude(p => p.ProductType)
                .FirstOrDefault(p => p.Id == id);

            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        [HttpPost]
        public IActionResult AddNote(Note model)
        {
            var patient = _context.Patients.Include(p => p.Notes).FirstOrDefault(p => p.Id == model.PatientId);
            if (patient == null) return NotFound();


            model.CreatedDate = DateTime.Now;

            _context.Notes.Add(model);
            _context.SaveChanges();
            // Yönlendirmeyi hash ile yap
            //var url = Url.Action("Details", "Patient", new { id = model.PatientId });
            //return Redirect(url + "#notes");  // Hash değerini sonradan ekliyoruz
            return RedirectToAction("Detail", new { id = model.PatientId });
        }

        [HttpPost]
        public IActionResult DeleteNote(Guid noteId, Guid patientId)
        {
            var patient = _context.Patients.Include(p => p.Notes).FirstOrDefault(p => p.Id == patientId);
            if (patient == null)
            {
                return NotFound();
            }

            var note = patient.Notes.FirstOrDefault(n => n.Id == noteId);
            if (note != null)
            {
                _context.Notes.Remove(note);
                _context.SaveChanges();
                return Json(new { success = true });
            }

            return Json(new { success = false });
        }

        [HttpPost]
        public IActionResult AddPayment(Guid PatientId, Guid SalesId, decimal Amount)
        {
            //var patient = _context.Patients.Include(p => p.Payments).FirstOrDefault(p => p.Id == PatientId);
            var satis = _context.Sales.FirstOrDefault(x => x.IsActive && x.Id == SalesId && x.PatientId == PatientId);
            if (satis == null) return NotFound();
            satis.CreatedDate = DateTime.UtcNow;
            satis.PaymentAmount += Amount;
            satis.RemainingAmount = satis.Price - satis.PaymentAmount;
            if (satis.RemainingAmount < 0)
            {
                // ViewBag.NotMines = "Ödeme Miktarınız kalan Bakiyenizden Fazla Olamaz";
                //TempData["NotMines"] = "Ödeme Miktarınız kalan bakiyenizden fazla olamaz!";
                // return RedirectToAction("Detail", new { id = PatientId });
                return Json(new { success = false, message = "Ödeme miktarınız kalan bakiyeden fazla olamaz!" });
            }

            _context.Payments.Add(new Payment
            {
                PatientId = PatientId,
                SalesId = satis.Id,
                Amount = Amount,
                PaymentDate = DateTime.UtcNow
            });

            _context.Sales.Update(satis);
            _context.SaveChanges();
            // return RedirectToAction("Detail", new { id = PatientId }) ;
            return Json(new { success = true });
        }

        #endregion

    }
}