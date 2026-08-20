using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtifyBE.Models
{
    public class BookingAddOns
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }

        [ForeignKey(nameof(Bookings))]
        [Column("BOOKING_ID")]
        public long BookingId { get; set; }
        public Bookings? Bookings { get; set; }

        [ForeignKey(nameof(EquipmentAddOns))]
        [Column("EQUIPMENT_ID")]
        public long EquipmentId { get; set; }
        public EquipmentAddOns? Equipment { get; set; }

        [Required]
        [Column("QUANTITY")]
        public int Quantity { get; set; }

        [Required]
        [Column("UNIT_PRICE")]
        public decimal UnitPrice { get; set; }

        [Required]
        [Column("SUB_TOTAL")]
        public decimal SubTotal { get; set; }

    }
}
