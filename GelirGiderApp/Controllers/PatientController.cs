
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
            var hastalar = await _context.Patients.ToListAsync();
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
        public async Task<IActionResult> Create([Bind("Id,Ad,Soyad,DogumTarihi,Telefon,Email,Adres")] Patient hasta)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hasta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(hasta);
        }

        // GET: Hasta/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hasta = await _context.Patients.FindAsync(id);
            if (hasta == null)
            {
                return NotFound();
            }
            return View(hasta);
        }

        // POST: Hasta/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Ad,Soyad,DogumTarihi,Telefon,Email,Adres")] Patient hasta)
        {
            if (id != hasta.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hasta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HastaExists(hasta.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(hasta);
        }

        // GET: Hasta/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hasta = await _context.Patients.FirstOrDefaultAsync(m => m.Id == id);
            if (hasta == null)
            {
                return NotFound();
            }

            return View(hasta);
        }

        // POST: Hasta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var hasta = await _context.Patients.FindAsync(id);
            if (!HastaExists(hasta.Id))
            {
                return NotFound();
            }
            _context.Patients.Remove(hasta);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HastaExists(Guid id)
        {
            return _context.Patients.Any(e => e.Id == id);
        }
    }
}

