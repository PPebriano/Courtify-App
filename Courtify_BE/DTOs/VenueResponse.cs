using CourtifyBE.Models;

namespace CourtifyBE.DTOs
{
    public class VenueResponse
    {
        public long Id { get; set; }
        public string NameVenue { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string PhoneNumber {  get; set; } = string.Empty;
        public bool IsActive { get; set; }

        public VenueResponse(Venues v)
        {
            Id = v.Id;
            NameVenue = v.NameVenue;
            Address = v.Address;
            PhoneNumber = v.PhoneNumber;
            IsActive = v.IsActive;
        }
    }
}
