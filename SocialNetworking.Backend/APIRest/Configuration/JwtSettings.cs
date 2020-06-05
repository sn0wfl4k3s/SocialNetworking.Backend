namespace WebAPI.Configuration
{
    public class JwtSettings
    {
        public string SigningKey { get; set; }
        public double ValidTokenMinutes { get; set; }
    }
}
