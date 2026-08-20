using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtifyBE.Models
{
    public class PaymentReceipt
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }

        [ForeignKey(nameof(Bookings))]
        [Column("BOOKING_ID")]
        public long BookingId { get; set; }
        public Bookings? Bookings { get; set; }

        [Required]
        [Column("PAYMENT_METHOD")]
        public string PaymentMethod { get; set; } = string.Empty;

        [Required]
        [Column("RECEIPT_NUMBER")]
        public string ReceiptNumber { get; set; } = string.Empty;

        [Required]
        [Column("STATUS")]
        public PaymentReceiptStatus Status { get; set; } = PaymentReceiptStatus.UNPAID;
    }
}
