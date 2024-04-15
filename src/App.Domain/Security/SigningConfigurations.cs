using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;

namespace Domain.Security;

public class SigningConfigurations
{
    public SecurityKey Key { get; set; }

    public SigningCredentials SigningCredentials { get; set; }

    public SigningConfigurations()
    {
        using (var provider = new RSACryptoServiceProvider(512) )
        {
            Key = new RsaSecurityKey(provider.ExportParameters(true));
        }
					
        SigningCredentials = new SigningCredentials(Key,   
            SecurityAlgorithms.RsaSha512Signature);
    }
}