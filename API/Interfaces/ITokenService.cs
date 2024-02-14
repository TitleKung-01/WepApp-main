using API.Entities;

namespace api;

public interface ITokenService
{
    Task<string> CreateToken(AppUser user);
}
