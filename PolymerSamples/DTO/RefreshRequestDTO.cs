namespace PolymerSamples.DTO
{
    public class RefreshRequestDTO
    {
        public required string JwtToken { get; set; }
        public required string RefreshToken { get; set; }
    }
}
