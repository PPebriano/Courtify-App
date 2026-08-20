using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtifyBE.Models
{
    public class EquipmentAddOns
    {
        [Required]
        [Column("ID")]
        public long Id { get; set; }

        [Required]
        [Column("ITEM_NAME")]
        public string ItemName { get; set; } = string.Empty;

        [Required]
        [Column("RENTAL_FEE")]
        public decimal RentalFee { get; set; }

        [Required]
        [Column("STOCK")]
        public int Stock {  get; set; }

        public ICollection<BookingAddOns> BookingAddOns { get; set; } = new List<BookingAddOns>();

    }
}
