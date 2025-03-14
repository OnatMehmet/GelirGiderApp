using GelirGiderApp.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace GelirGiderApp.Services
{
    public class SidebarService
    {
        private readonly ApplicationDbContext _context;

        public SidebarService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Page>> GetUserPagesAsync(ApplicationUser user)
        {
            var userPermissions = await _context.UserPermissions
                .Where(p => p.UserId == user.Id && p.CanView) // Kullanıcının görebileceği sayfalar
                .Select(p => p.PageName)
                .ToListAsync();

            return await _context.Pages
                .Where(p => userPermissions.Contains(p.Name) && p.IsActive)
                .ToListAsync();
        }
    }

}
