using CourtifyBE.Models;
using System.Text.Json.Serialization;

namespace CourtifyBE.DTOs
{
    // DTO untuk Item Add-On
    public class BookingAddOnResponse
    {
        public long Id { get; set; }
        public long BookingId { get; set; }

        [JsonPropertyName("equipmentAddOnsId")]
        public long EquipmentAddOnsId { get; set; }

        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        [JsonPropertyName("subtotal")]
        public decimal Subtotal { get; set; }

        [JsonPropertyName("equipmentName")]
        public string EquipmentName { get; set; } = string.Empty;
    }

    // DTO Khusus GET /api/bookings (Get All) -> TANPA bookingAddOns
    public class BookingListResponse
    {
        public long Id { get; set; }
        public string BookingCode { get; set; } = string.Empty;
        public long AdminId { get; set; }
        public long CourtId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public DateTime BookingDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        [JsonPropertyName("total_hours")]
        public int TotalHours { get; set; }

        [JsonPropertyName("base_amount")]
        public decimal BaseAmount { get; set; }

        [JsonPropertyName("grandTotal")]
        public decimal TotalAmount { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public BookingStatus Status { get; set; }

        public DateTime CreatedAt { get; set; }
    }

    // DTO Khusus GET /api/bookings/{id} (Get By ID) -> DENGAN bookingAddOns
    public class BookingDetailResponse
    {
        public long Id { get; set; }
        public string BookingCode { get; set; } = string.Empty;
        public long AdminId { get; set; }
        public long CourtId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public DateTime BookingDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        [JsonPropertyName("total_hours")]
        public int TotalHours { get; set; }

        [JsonPropertyName("base_amount")]
        public decimal BaseAmount { get; set; }

        [JsonPropertyName("grandTotal")]
        public decimal TotalAmount { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public BookingStatus Status { get; set; }

        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("bookingAddOns")]
        public List<BookingAddOnResponse> BookingAddOns { get; set; } = new();
    }
}