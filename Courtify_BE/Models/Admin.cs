using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtifyBE.Models
{
    public class Admin
    {
        [Key]
        [Column("ID")]
        public long Id {  get; set; }

        [Required]
        [Column("USERNAME")]
        public string Username { get; set; } = string.Empty;

        [Required]
        [Column("PASSWORD")]
        public string Password { get; set; } = string.Empty;

        [Required]
        [Column("NAME")]
        public string Name {  get; set; } = string.Empty;

        [Required]
        [Column("CREATED_AT")]
        public DateTime CreatedAt { get; set; }

        public ICollection<Bookings> Bookings { get; set; } = new List<Bookings>();
    }
}
