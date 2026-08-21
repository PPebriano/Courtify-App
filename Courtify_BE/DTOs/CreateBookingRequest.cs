namespace CourtifyBE.DTOs
{
    public class CreateBookingRequest
    {
        public long AdminId { get; set; }
        public long CourtsId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public DateTime BookingDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string PaymentMethod { get; set; } = "Cash"; //  Cash, Transfer, QRIS
        public List<AddOnItemRequest> AddOns { get; set; } = new();
    }
}
