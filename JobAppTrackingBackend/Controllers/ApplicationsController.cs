using JobAppTrackingBackend.Data;
using JobAppTrackingBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace JobAppTrackingBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApplicationsController : ControllerBase
    {
        private readonly AppDbContext _db;
        public ApplicationsController(AppDbContext db) => _db = db;

        [HttpGet("status-counts/{userId}")]
        public async Task<IActionResult> StatusCounts(int userId)
        {
            var counts = await _db.Applications
                .Where(a => a.UserId == userId)
                .GroupBy(a => a.Status)
                .Select(g => new { Status = g.Key, Count = g.Count() })
                .ToListAsync();
            var res = new
            {
                Applied = counts.FirstOrDefault(c => c.Status == "Applied")?.Count ?? 0,
                Accepted = counts.FirstOrDefault(c => c.Status == "Accepted")?.Count ?? 0,
                Rejected = counts.FirstOrDefault(c => c.Status == "Rejected")?.Count ?? 0
            };

            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> AddApplication([FromBody] AddApplicationRequest req)
        {
            var user = await _db.Users.FindAsync(req.UserId);
            if (user == null) return BadRequest("User not found");
            var job = await _db.Jobs.FindAsync(req.JobId);
            if (job == null)
            {
                if (string.IsNullOrWhiteSpace(req.CompanyName) && string.IsNullOrWhiteSpace(req.Position)) return BadRequest("Provide Company and Position");
                job = new Job { JobId = req.JobId, CompanyName = req.CompanyName, Position = req.Position };
                _db.Jobs.Add(job);
                await _db.SaveChangesAsync();
            }

            var application = new Application
            {
                ApplicationId = req.ApplicationId,
                UserId = req.UserId,
                JobId = job.JobId,
                Status = req.Status ?? "Applied",
                AppliedDate = req.AppliedDate ?? DateTime.UtcNow,
                Priority = req.Priority ?? "Medium"

            };

            _db.Applications.Add(application);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetApplication), new { id = application.ApplicationId }, application);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetApplication(int id)
        {
            var app = await _db.Applications.FindAsync(id);
            if (app == null) return NotFound();
            return Ok(app);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApplication(int id)
        {
            var app = await _db.Applications.FindAsync(id);
            if (app == null) return NotFound();
            _db.Applications.Remove(app);
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("top-companies/{userId}")]
        public async Task<IActionResult> TopCompaniesForUser(int userId)
        {
            var res = await _db.Applications
                .Where(a => a.UserId == userId)
                .Join(_db.Jobs, a => a.JobId, j => j.JobId, (a, j) => j.CompanyName)
                .GroupBy(c => c)
                .Select(g => new { Company = g.Key, Count = g.Count() })
                .OrderByDescending(x => x.Count)
                .Take(10)
                .ToListAsync();
            return Ok(res);
        }

        [HttpGet("user-monthly-applications/{userId}")]
        public async Task<IActionResult> UserMonthlyApplications(int userId)
        {
            var res = await _db.Applications
                .Where(a => a.UserId == userId && a.AppliedDate != null)
                .GroupBy(a => new { Year = a.AppliedDate!.Value.Year, Month = a.AppliedDate!.Value.Month })
                .Select(g => new { g.Key.Year, g.Key.Month, Count = g.Count() })
                .OrderBy(g => new { g.Year, g.Month })
                .ToListAsync();
            return Ok(res);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditApplication(int id, [FromBody] EditApplicationRequest req)
        {
            var app = await _db.Applications.FindAsync(id);
            if (app == null) return NotFound();
            app.Status = req.Status ?? app.Status;
            app.Priority = req.Priority ?? app.Priority;
            if (req.AppliedDate != null) app.AppliedDate = req.AppliedDate;
            await _db.SaveChangesAsync();
            return Ok(app);
        }
    }

    public record AddApplicationRequest(
        int ApplicationId,
        int UserId,
        int JobId,
        string? CompanyName,
        string? Position,
        string? Status,
        DateTime? AppliedDate,
        string? Priority
    );
    
    public record EditApplicationRequest(
        string? Status,
        string? Priority,
        DateTime? AppliedDate
    );
}