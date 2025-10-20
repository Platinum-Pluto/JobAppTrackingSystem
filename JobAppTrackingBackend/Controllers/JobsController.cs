using JobAppTrackingBackend.Data;
using JobAppTrackingBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;


namespace JobAppTrackingBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JobsController : ControllerBase
    {
        private readonly AppDbContext _db;
        public JobsController(AppDbContext db) => _db = db;

        [HttpPost]
        public async Task<IActionResult> CreateJob([FromBody] Job job)
        {
            _db.Jobs.Add(job);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetJob), new { id = job.JobId }, job);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetJob(int id)
        {
            var job = await _db.Jobs.FindAsync(id);
            if (job == null) return NotFound();
            return Ok(job);
        }

        [HttpDelete("{jobId}")]
        public async Task<IActionResult> DeleteJob(int jobId)
        {
            var job = await _db.Jobs.FindAsync(jobId);
            if (job == null) return NotFound();
            var applications = _db.Applications.Where(a => a.JobId == jobId);
            _db.Applications.RemoveRange(applications);
            _db.Jobs.Remove(job);
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("top-jobs")]
        public async Task<IActionResult> TopJobs()
        {
            var res = await _db.Applications
                .GroupBy(a => a.JobId)
                .Select(g => new { JobId = g.Key, Count = g.Count() })
                .OrderByDescending(x => x.Count)
                .Take(5)
                .Join(_db.Jobs, g => g.JobId, j => j.JobId, (g, j) => new { j.JobId, j.CompanyName, j.Position, g.Count })
                .ToListAsync();
            return Ok(res);
        }

        [HttpGet("avg-applications-per-job")]
        public async Task<IActionResult> AvgApplicationsPerJob()
        {
            var totalApplications = await _db.Applications.CountAsync();
            var totalJobs = await _db.Jobs.CountAsync();
            double avg = totalJobs == 0 ? 0 : (double)totalApplications / totalJobs;
            return Ok(new { totalApplications, totalJobs, averagePerJob = avg });
        }

        [HttpGet("top-companies")]
        public async Task<IActionResult> TopCompanies()
        {
            var res = await _db.Applications
                .Join(_db.Jobs, a => a.JobId, j => j.JobId, (a, j) => j.CompanyName)
                .Where(c => c != null)
                .GroupBy(c => c)
                .Select(g => new { Company = g.Key, Count = g.Count() })
                .OrderByDescending(x => x.Count)
                .Take(5)
                .ToListAsync();
            return Ok(res);
        }





    }
}