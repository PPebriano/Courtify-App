namespace CourtifyBE.DTOs
{
    public class CourtDto
    {
        public long CourtId { get; set; }
        public string CourtName { get; set; } = string.Empty;
        public CourtCategoryDto CourtCategory { get; set; } = null!;
        public decimal HourlyRate { get; set; }
        public bool IsAvailable { get; set; }
    }
}
