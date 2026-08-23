namespace CourtifyBE.DTOs
{
    public class CreateBookingRequest
    {
        public long AdminId { get; set; }
        public long CourtId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public DateTime BookingDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public List<AddOnItemRequest> AddOns { get; set; } = new();
    }
}
