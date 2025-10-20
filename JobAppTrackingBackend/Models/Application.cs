using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobAppTrackingBackend.Models
{
    [Table("applications")]
    public class Application
    {
        [Key]
        [Column("application_id")]
        public int ApplicationId { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        [Column("job_id")]
        public int JobId { get; set; }

        [Column("status")]
        public string? Status { get; set; }

        [Column("applied_date")]
        public DateTime? AppliedDate { get; set; }

        [Column("priority")]
        public string? Priority { get; set; }
    }
}