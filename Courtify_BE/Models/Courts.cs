using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtifyBE.Models
{
    public class Courts
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }

        [ForeignKey(nameof(Venues))]
        [Column("VENUE_ID")]
        public long VenueId { get; set; }
        public Venues? Venues { get; set; }

        [ForeignKey(nameof(CourtCategory))]
        [Column("COURT_CATEGORY_ID")]
        public long CourtCategoryId { get; set; }
        public CourtCategory? CourtCategory { get; set; }

        [ForeignKey(nameof(Courts))]
        [Column("COURT_NAME")]
        public string CourtName { get; set;} = string.Empty;

        [Required]
        [Column("HOURLY_RATE")]
        public decimal HourlyRate { get; set; }

        [Required]
        [Column("IS_AVAILABLE")]
        public bool IsAvailable { get; set; }

        public ICollection<Bookings> Bookings { get; set; } = new List<Bookings>();  
    }
}
