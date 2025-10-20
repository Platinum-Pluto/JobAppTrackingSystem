using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobAppTrackingBackend.Models{
    [Table("users")]
    public class User{
        [Key]
        [Column("user_id")]
        public int UserId { get; set; }

        [Column("name")]
        public string? Name { get; set; }

        [Column("email")]
        public string? Email { get; set; }

        [Column("password")]
        public string? Password { get; set; }

        [Column("country")]
        public string? Country { get; set; }

        [Column("district")]
        public string? District { get; set; }

        [Column("created_at")]
        public DateTime? CreatedAt { get; set; }

    }
}