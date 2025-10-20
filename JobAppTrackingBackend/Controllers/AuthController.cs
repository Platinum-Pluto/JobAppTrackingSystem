using JobAppTrackingBackend.Data;
using JobAppTrackingBackend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobAppTrackingBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _db;
        public AuthController(AppDbContext db) => _db = db;

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest req)
        {
            if (string.IsNullOrWhiteSpace(req.Email) || string.IsNullOrWhiteSpace(req.Password)) return BadRequest("Email and Password required");

            var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == req.Email && u.Password == req.Password);
            if (user != null)
            {
                return Ok(new { role = "user", data = user });
            }

            var admin = await _db.Admins.FirstOrDefaultAsync(a => a.Email == req.Email && a.Password == req.Password);
            if (admin != null)
            {
                return Ok(new { role = "admin", data = admin });
            }

            return Ok(null);

        }
    }
    public record LoginRequest(string Email, String Password);
}