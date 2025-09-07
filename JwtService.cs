using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace JwtCraft;

public class JwtService : ITokenService
{
    public string GerenateAcessRefreshToken()
    {
        var bytes = new byte[128];
        RandomNumberGenerator.Fill(bytes);
        return Convert.ToBase64String(bytes);
    }

    public string GerenateAcessToken(IEnumerable<Claim> claims, IConfiguration configuration)
    {
        var bytesKey = Encoding.UTF8.GetBytes(JwtOptions.SecretKey);
        var credentials = new SigningCredentials(
            new SymmetricSecurityKey(bytesKey), SecurityAlgorithms.HmacSha256);
        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(JwtOptions.TokenValidInMinutes),
            Audience = JwtOptions.Audience,
            Issuer = JwtOptions.Issuer,
            SigningCredentials = credentials
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public ClaimsPrincipal GetClaimsPrincipalExpiredToken(string token, IConfiguration configuration)
    {
        var bytesKey = Encoding.UTF8.GetBytes(JwtOptions.SecretKey);
        var tokenHandler = new JwtSecurityTokenHandler();
        var claimsPrincipal = tokenHandler.ValidateToken(token, new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidIssuer = JwtOptions.Issuer,
            ValidateAudience = true,
            ValidAudience = JwtOptions.Audience,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(bytesKey)
        }, out var secretToken);
        
        if (secretToken is not JwtSecurityToken jwt || 
            !jwt.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
        {
            throw new SecurityTokenException("Token invalid");
        }
        return claimsPrincipal;
    }
}