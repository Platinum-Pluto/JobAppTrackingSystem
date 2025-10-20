using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobAppTrackingBackend.Models
{
    [Table("Jobs")]
    public class Job
    {
        [Key]
        [Column("job_id")]
        public int JobId { get; set; }

        [Column("company_name")]
        public string? CompanyName { get; set; }

        [Column("position")]
        public string? Position { get; set; }
    }
}