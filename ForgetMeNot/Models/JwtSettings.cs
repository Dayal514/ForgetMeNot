namespace ForgetMeNot.Models
{
    public class JwtSettings
    {
        public string Issuer { get; set; } = "";
        public string Audience { get; set; } = "";
        public string SigningKey { get; set; } = "";
        public int ExpiryMinutes { get; set; } = 10;
    }
}
