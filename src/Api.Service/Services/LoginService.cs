
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using Api.Domain.Entities;
using Domain.Dtos;
using Domain.Interfaces.Services.User;
using Domain.Repository;
using Domain.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Service.Services;

public class LoginService : IloginService
{
    private IUserRepository _repository;

    private SigningConfigurations _signingConfigurations;
    private TokenConfiguration _tokenConfigurations;
    private IConfiguration _Configuration;


    public LoginService(IUserRepository repository,
        SigningConfigurations signingConfigurations,
        TokenConfiguration tokenConfigurations,
        IConfiguration configuration)
    {
        _repository = repository;
        _signingConfigurations = signingConfigurations;
        _tokenConfigurations = tokenConfigurations;
        _Configuration = configuration;
    }

    public async Task<object> FindByLogin(LoginDto user)
    {
        var baseUser = new UserEntity();

        if (user != null && !string.IsNullOrWhiteSpace(user.Email))
        {
            baseUser = await _repository.FindByLogin(user.Email);
            if (baseUser == null)
            {
                return new
                {
                    authenticated = false,
                    message = "Falha ao autenticar"
                };
            }
            else
            {
                var identity = new ClaimsIdentity(
                    new GenericIdentity(baseUser.Email),
                    new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.UniqueName, user.Email),
                    }
                );

                DateTime createDate = DateTime.Now;
                DateTime expirationDate = createDate + TimeSpan.FromSeconds(_tokenConfigurations.Seconds);

                var handler = new JwtSecurityTokenHandler();
                string token = CreateToken(identity, createDate, expirationDate, handler);
                return SuccessObject(createDate, expirationDate, token, user);
            }
        }
        else
        {
            return null;
        }
    }
    private string CreateToken	(ClaimsIdentity identity, DateTime createDate, DateTime expirationDate, JwtSecurityTokenHandler handler)
    {
        var securityToken = handler.CreateToken(new SecurityTokenDescriptor
        {
            Issuer = _tokenConfigurations.Issuer,
            Audience = _tokenConfigurations.Audience,
            SigningCredentials = _signingConfigurations.SigningCredentials,
            Subject = identity,
            NotBefore = createDate,
            Expires = expirationDate,					 
        });
					 
        var token = handler.WriteToken(securityToken);
        return token;				
    }
    
    private object SuccessObject(DateTime createDate, DateTime expiationDate, string token, LoginDto user)
    {
        return new 
        {
            authenticated = true,
            created = createDate.ToString("yyyy-MM-dd HH:mm:ss"),
            expiration = expiationDate.ToString("yyyy-MM-dd HH:mm:ss"),
            acessToken = token,
            userName = user.Email,
            message = "Usuário logado com sucesso"
        };	
    }
}   