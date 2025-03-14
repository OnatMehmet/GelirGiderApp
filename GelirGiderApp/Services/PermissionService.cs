using GelirGiderApp.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace GelirGiderApp.Services
{
    public class PermissionService
    {
        private readonly ApplicationDbContext _context;

        public PermissionService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<UserPermission> GetUserPermissionAsync(string userId, string pageName)
        {
            return await _context.UserPermissions
                .FirstOrDefaultAsync(p => p.UserId == userId && p.PageName == pageName);
        }
    }

}
