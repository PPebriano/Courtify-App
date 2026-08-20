namespace CourtifyBE.DTOs
{
    public class CreateBookingRequest
    {
        public long CourtsId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public DateTime BookingDate { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public string PaymentMethod { get; set; } = "Cash"; //  Cash, Transfer, QRIS
        public List<AddOnItemRequest> AddOns { get; set; } = new();
    }
}
