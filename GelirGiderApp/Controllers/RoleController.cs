using GelirGiderApp.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GelirGiderApp.Controllers
{
   // [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RoleController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Rol listesi
        public IActionResult Index()
        {
            var roles = _context.Roles.ToList();
            return View(roles);
        }

        // Yeni rol ekleme
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Role role)
        {
            if (ModelState.IsValid)
            {
                role.CreatedDate = DateTime.UtcNow;
                _context.Roles.Add(role);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(role);
        }

        // Rol düzenleme
        public IActionResult Edit(Guid id)
        {
            var role = _context.Roles.FirstOrDefault(r => r.Id == id);
            if (role == null) return NotFound();
            return View(role);
        }

        [HttpPost]
        public IActionResult Edit(Role role)
        {
            if (ModelState.IsValid)
            {
                _context.Roles.Update(role);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(role);
        }

        // Rol silme
        public IActionResult Delete(Guid id)
        {
            var role = _context.Roles.FirstOrDefault(r => r.Id == id);
            if (role == null) return NotFound();

            role.IsDeleted = true;
            role.IsActive = false;
            _context.Roles.Update(role);
            //_context.Roles.Remove(role);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
