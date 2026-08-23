using CourtifyBE.Models;
using System.Text.Json.Serialization;

namespace CourtifyBE.DTOs
{
    public class UpdateBookingStatusRequest
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public BookingStatus Status { get; set; }
    }

    public class UpdateBookingStatusResponse
    {
        public long BookingId { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
