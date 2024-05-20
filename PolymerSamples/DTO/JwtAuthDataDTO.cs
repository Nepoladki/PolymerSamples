namespace PolymerSamples.DTO
{
    public class JwtAuthDataDTO
    {
        public required string JwtToken { get; set; }
        public DateTime Expiration { get; set; }
        public required string RefreshToken { get; set; }
        public DateTime RefreshExpires { get; set; }
    }
}
