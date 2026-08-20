using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtifyBE.Models
{
    public class CourtCategory
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }

        [Required]
        [Column("CATEGORY")]
        public string Category { get; set; } = string.Empty;

        [Column("DESCRIPTION")]
        public string? Description { get; set; }

        public ICollection<Courts> Courts { get; set; } = new List<Courts>();

    }
}
