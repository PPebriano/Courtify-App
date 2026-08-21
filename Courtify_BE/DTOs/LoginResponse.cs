namespace CourtifyBE.DTOs
{
    public class LoginResponse
    {
        public string Status { get; set; } = "success";
        public string Token { get; set; } = string.Empty;
        public UserSummary Admin { get; set; } = new();
    }

    public class UserSummary
    {
        public long Id { get; set; }
        public string Nama { get; set; } = string.Empty;
    }
}
