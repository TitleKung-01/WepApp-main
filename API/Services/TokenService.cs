using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Entities;
using Microsoft.IdentityModel.Tokens;

namespace api;

public class TokenService : ITokenService
{
  readonly SymmetricSecurityKey _privateKey;
  public TokenService(IConfiguration config)
  {
    _privateKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]!));
  }
  public string CreateToken(AppUser user)
  {
    var claims = new List<Claim> {
            new(JwtRegisteredClaimNames.NameId, user.UserName!)
        };

    var credentials = new SigningCredentials(_privateKey, SecurityAlgorithms.HmacSha256Signature);

    var tokenDescriptor = new SecurityTokenDescriptor
    {
      Subject = new ClaimsIdentity(claims),
      Expires = DateTime.Now.AddDays(3),
      SigningCredentials = credentials
    };

    var tokenHandler = new JwtSecurityTokenHandler();

    var token = tokenHandler.CreateToken(tokenDescriptor);

    return tokenHandler.WriteToken(token);
  }
}
