using System.Security.Claims;
using Microsoft.Extensions.Configuration;

namespace JwtCraft;

public interface ITokenService
{
    string GerenateAcessRefreshToken();
    string GerenateAcessToken(IEnumerable<Claim>claims,IConfiguration configuration);
    ClaimsPrincipal GetClaimsPrincipalExpiredToken(string token,IConfiguration configuration);
}