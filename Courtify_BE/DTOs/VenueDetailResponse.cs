namespace CourtifyBE.DTOs
{
    public class VenueDetailResponse
    {
        public long VenueId { get; set; }
        public string VenueName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public List<CourtDto> Courts { get; set; } = new();
    }

    

    
}
 