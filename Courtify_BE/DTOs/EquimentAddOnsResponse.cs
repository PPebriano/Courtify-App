namespace CourtifyBE.DTOs
{
    public class EquimentAddOnsResponse
    {
        public long Id { get; set; }
        public string? ItemName { get; set; } 
        public decimal RentalFee { get; set; }
        public int Stock {  get; set; }

    }
}
