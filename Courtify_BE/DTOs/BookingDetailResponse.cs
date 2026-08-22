using CourtifyBE.Models;
using System.Text.Json.Serialization;

namespace CourtifyBE.DTOs
{
    public class BookingDetailResponse
    {
        public long Id { get; set; }
        public string BookingCode { get; set; }
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
        public BookingStatus Status { get; set; } = BookingStatus.ACTIVE;
        public DateTime CreatedAt { get; set; }


    }
}
