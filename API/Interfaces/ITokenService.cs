using API.Entities;

namespace api;

public interface ITokenService
{
  string CreateToken(AppUser user);
}
