using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace api;

public class TokenService : ITokenService
{
  readonly SymmetricSecurityKey _privateKey;
  readonly UserManager<AppUser> _userManager;
  public TokenService(UserManager<AppUser> userManager, IConfiguration config)
  {
    _privateKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]!));
    _userManager = userManager;
  }
  public async Task<string> CreateToken(AppUser user)
  {
    var claims = new List<Claim> {
            new(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
            new(JwtRegisteredClaimNames.UniqueName, user.UserName!),
        };
    var roles = await _userManager.GetRolesAsync(user);
    claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

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
