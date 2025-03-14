using GelirGiderApp.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace GelirGiderApp.Controllers
{
    //[Authorize(Roles = "Admin")] // Sadece Admin yetkisi olanlar erişebilir
    public class RoleController : Controller
    {
       private readonly RoleManager<IdentityRole> _roleManager;
      

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        // ROL LİSTESİNİ GÖRÜNTÜLEME
        public IActionResult Index()
        {
            var roles = _roleManager.Roles.ToList();
            return View(roles);
        }

        // YENİ ROL EKLEME (GET)
        public IActionResult Create()
        {
            return View();
        }

        // YENİ ROL EKLEME (POST)
        [HttpPost]
        public async Task<IActionResult> Create(string roleName)
        {
            if (!string.IsNullOrEmpty(roleName))
            {
                var roleExists = await _roleManager.RoleExistsAsync(roleName);
                if (!roleExists)
                {
                    await _roleManager.CreateAsync(new IdentityRole(roleName));
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", "Bu rol zaten mevcut.");
            }
            return View();
        }

        // ROL GÜNCELLEME (GET)
        public async Task<IActionResult> Edit(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null) return NotFound();
            return View(role);
        }

        // ROL GÜNCELLEME (POST)
        [HttpPost]
        public async Task<IActionResult> Edit(string id, string name)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null) return NotFound();

            if (!string.IsNullOrEmpty(name))
            {
                role.Name = name;
                await _roleManager.UpdateAsync(role);
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Rol ismi boş olamaz.");
            return View(role);
        }

        // ROL SİLME
        public async Task<IActionResult> Delete(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return Json(new { success = false });
            }
            await _roleManager.DeleteAsync(role);
            return Json(new { success = true });
            //return RedirectToAction("Index");
        }
    }
}
