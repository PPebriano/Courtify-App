namespace CourtifyBE.Exceptions
{
    public class CourtNotAvailableException : Exception
    {
        public CourtNotAvailableException(string courtCode) : base($"Lapangan {courtCode} sudah dipesan orang lain") { } 
    }
}
