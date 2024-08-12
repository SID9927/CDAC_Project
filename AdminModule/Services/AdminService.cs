using AdminModule.DataDb;
using AdminModule.Model;
using Microsoft.EntityFrameworkCore;

namespace AdminModule.Services
{
    public class AdminService
    {
        private readonly AdminContext _context;

        public AdminService(AdminContext context)
        {
            _context = context;
        }

        public async Task<Admin> GetAdminByEmail(string email)
        {
            return await _context.Admins.FirstOrDefaultAsync(a => a.Email == email);
        }

        public async Task<IEnumerable<Admin>> GetAllAdmins()
        {
            return await _context.Admins.ToListAsync();
        }

        public async Task<Admin> CreateAdmin(Admin admin)
        {
            _context.Admins.Add(admin);
            await _context.SaveChangesAsync();
            return admin;
        }
    }
}
