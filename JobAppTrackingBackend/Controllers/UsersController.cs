using System.Net.Http.Headers;
using JobAppTrackingBackend.Data;
using JobAppTrackingBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace JobAppTrackingBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController: ControllerBase
    {
        private readonly AppDbContext _db;
        public UsersController(AppDbContext db) => _db = db;
        [HttpGet("{userId}/applications")]
        public async Task<IActionResult> GetUserApplications(int userId)
        {
            var applications = await _db.Applications
                .Where(a => a.UserId == userId)
                .Join(_db.Jobs, a => a.JobId, j => j.JobId, (a, j) => new
                {
                    a.ApplicationId,
                    a.UserId,
                    JobCompany = j.CompanyName,
                    JobPosition = j.Position,
                    a.Status,
                    a.AppliedDate,
                    a.Priority
                })
                .ToListAsync();
            return Ok(applications);
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUser(int userId, [FromBody] User update)
        {
            var user = await _db.Users.FindAsync(userId);
            if (user == null) return NotFound();

            user.Name = update.Name ?? user.Name;
            user.Email = update.Email ?? user.Email;
            user.Password = update.Password ?? user.Password;
            user.Country = update.Country ?? user.Country;
            user.District = update.District ?? user.District;

            await _db.SaveChangesAsync();
            return Ok(user);
        }

        [HttpGet("recent-signups")]
        public async Task<IActionResult> GetRecentSignups()
        {
            var hasCreatedAt = await _db.Database.ExecuteSqlRawAsync("SELECT 1");
            var res = await _db.Users
                .OrderByDescending(u => u.CreatedAt ?? DateTime.MinValue)
                .Take(5)
                .ToListAsync();
            return Ok(res);
        }


        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            var user = await _db.Users.FindAsync(userId);
            if (user == null) return NotFound();

            var applications = _db.Applications.Where(a => a.UserId == userId);
            _db.Applications.RemoveRange(applications);
            _db.Users.Remove(user);
            await _db.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("top-districts")]
        public async Task<IActionResult> TopDistricts()
        {
            var res = await _db.Users
                .Where(u => u.District != null)
                .GroupBy(u => u.District)
                .Select(g => new { District = g.Key, Count = g.Count() })
                .OrderByDescending(x => x.Count)
                .Take(5)
                .ToListAsync();

            return Ok(res);
        }

        [HttpGet("new-users-6months")]
        public async Task<IActionResult> NewUsersPast6Months()
        {
            var sixM = DateTime.UtcNow.AddMonths(-5);
            var res = await _db.Users
                .Where(u => u.CreatedAt != null && u.CreatedAt >= sixM)
                .GroupBy(u => new { Year = u.CreatedAt!.Value.Year, Month = u.CreatedAt!.Value.Month })
                .Select(g => new { g.Key.Year, g.Key.Month, Count = g.Count() })
                .OrderByDescending(g => new { g.Year, g.Month })
                .ToListAsync();

            return Ok(res);
        }

        [HttpGet("totals")]
        public async Task<IActionResult> GetTotals()
        {
            var totalUsers = await _db.Users.CountAsync();
            var totalJobs = await _db.Jobs.CountAsync();
            var totalStatus = await _db.Applications
                .GroupBy(a => a.Status)
                .Select(g => new { Status = g.Key, Count = g.Count() })
                .ToListAsync();

            return Ok(new { totalUsers, totalJobs, totalStatus });
        }


        





    }
}