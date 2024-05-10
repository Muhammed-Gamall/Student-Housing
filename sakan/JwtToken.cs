namespace sakan
{
    public class JwtToken { 
        public string Issuer { get; set; }
        public string Audiance { get; set; }
        public int LifeTime { get; set; }
        public string SigningKey { get; set; }
    }

}
