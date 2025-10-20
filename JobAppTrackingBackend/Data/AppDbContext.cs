using JobAppTrackingBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace JobAppTrackingBackend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<User> Users => Set<User>();
        public DbSet<Admin> Admins => Set<Admin>();
        public DbSet<Job> Jobs => Set<Job>();
        public DbSet<Application> Applications => Set<Application>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}