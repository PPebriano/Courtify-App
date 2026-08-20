namespace CourtifyBE.DTOs
{
    public class BookingDetailResponse
    {
        public long Id { get; set; }
        public long BookingCode { get; set; }
        public long AdminId { get; set; }
        public long CourtId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public DateTime BookingDate { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public int TotalHours { get; set; }
        public decimal BaseAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }


    }
}
