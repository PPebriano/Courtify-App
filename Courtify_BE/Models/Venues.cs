using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CourtifyBE.Models
{
    public class Venues
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }

        [Required]
        [Column("NAME_VENUE")]
        public string NameVenue { get; set; } = string.Empty;

        [Column("ADDRESS")]
        public string? Address {  get; set; }

        [Required]
        [MaxLength(12)]
        [Column("PHONE_NUMBER")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        [Column("IS_ACTIVE")]
        public bool IsActive { get; set; }

        public ICollection<Courts> Courts { get; set; } = new List<Courts>();
    }
}
