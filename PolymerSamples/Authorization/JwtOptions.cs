namespace PolymerSamples.Authorization
{
    public class JwtOptions
    {
        public string SecretKey { get; set; } = string.Empty;
        public int ExpiresMinutes { get; set; }
        public int RefreshExpiresHours { get; set; }
    }
}