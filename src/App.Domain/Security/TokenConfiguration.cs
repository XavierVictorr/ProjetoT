namespace Domain.Security;

public class TokenConfiguration
{
    public string Audience { get; set; }
    
    public string Issuer { get; set; }
    
    public double Seconds { get; set; }
}