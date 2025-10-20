using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobAppTrackingBackend.Models{
    [Table("admin")]
    public class Admin{
        [Key]
        [Column("admin_id")]
        public int AdminId { get; set; }

        [Column("name")]
        public string? Name { get; set; }

        [Column("email")]
        public string? Email { get; set; }

        [Column("password")]
        public string? Password { get; set; }

    }
}