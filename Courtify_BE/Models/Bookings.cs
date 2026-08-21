using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtifyBE.Models
{
    public class Bookings
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }

        [Required]
        [Column("BOOKING_CODE")]
        public string BookingCode { get; set; } = string.Empty;

        [ForeignKey(nameof(Admin))]
        [Column("ADMIN_ID")]
        public long AdminId { get; set; }
        public Admin? Admin { get; set; }

        [ForeignKey(nameof(Courts))]
        [Column("COURTS_ID")]
        public long CourtsId { get; set; }
        public Courts? Courts { get; set; }

        [Required]
        [Column("CUSTOMER_NAME")]
        public string CustomerName { get; set; } = string.Empty;

        [Required]
        [Column("BOOKING_DATE")]
        public DateTime BookingDate { get; set; }

        [Required]
        [Column("START_TIME")]
        public TimeSpan StartTime { get; set; }

        [Required]
        [Column("END_TIME")]
        public TimeSpan EndTime { get; set; }

        [Required]
        [Column("TOTAL_HOURS")]
        public int TotalHours { get; set; }

        [Required]
        [Column("BASE_AMOUNT")]
        public decimal BaseAmount { get; set; }

        [Required]
        [Column("TOTAL_AMOUNT")]
        public decimal TotalAmount { get; set; }

        [Required]
        [Column("STATUS")]
        public string Status { get; set; } = string.Empty;

        [Required]
        [Column("CREATED_AT")]
        public DateTime CreatedAt { get; set; }

        public ICollection<PaymentReceipt> PaymentReceipts { get; set; } = new List<PaymentReceipt>();
        public ICollection<BookingAddOns> BookingAddOns { get; set; } = new List<BookingAddOns>();
    }
}
