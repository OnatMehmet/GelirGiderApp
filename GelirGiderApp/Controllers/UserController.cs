using GelirGiderApp.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GelirGiderApp.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Kullanıcı listesi
        public IActionResult Index()
        {
            var users = _context.Users.Include(u => u.Role).ToList();
            return View(users);
        }

        // Yeni kullanıcı ekleme
        public IActionResult Create()
        {
            ViewBag.Roles = _context.Roles.ToList();  // Rolleri dropdown list olarak göster
            return View();
        }

        [HttpPost]
        public IActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                //user.CreatedBy = user.Id;
                user.CreatedDate = DateTime.UtcNow;
                _context.Users.Add(user);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Roles = _context.Roles.ToList();
            return View(user);
        }

        // Kullanıcı düzenleme
        public IActionResult Edit(Guid id)
        {
            var user = _context.Users.Include(u => u.Role).FirstOrDefault(u => u.Id == id);
            if (user == null) return NotFound();
            ViewBag.Roles = _context.Roles.ToList();
            return View(user);
        }

        [HttpPost]
        public IActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                //user.UpdatedBy = user.Id;
                _context.Users.Update(user);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Roles = _context.Roles.ToList();
            return View(user);
        }

        // GET: user/Delete/5
        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return Json(new { success = false, message = "Kullanıcı bulunamadı!" });
            }
            user.IsActive = false;
            user.IsDeleted = true;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return Json(new { success = true, message = "Kullanıcı Silme işlemi başarıyla silindi!" });
        }
    }

}
