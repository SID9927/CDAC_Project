using AdminModule.Model;
using AdminModule.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdminModule.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly AdminService _adminService;

        public AdminController(AdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Admin>>> GetAllAdmins()
        {
            var admins = await _adminService.GetAllAdmins();
            return Ok(admins);
        }

        [HttpPost]
        public async Task<ActionResult<Admin>> CreateAdmin(AdminCreateModel model)
        {
            var admin = new Admin
            {
                Name = model.Name,
                Email = model.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password)
            };

            var createdAdmin = await _adminService.CreateAdmin(admin);
            return CreatedAtAction(nameof(GetAllAdmins), new { id = createdAdmin.Id }, createdAdmin);
        }

        // Add more admin-related endpoints as needed
    }

    public class AdminCreateModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}

