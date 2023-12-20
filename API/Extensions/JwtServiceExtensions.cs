using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace API.Extensions;

public static class JwtServiceExtensions
{
  public static IServiceCollection AddJWTService(this IServiceCollection services, IConfiguration conf)
  {
    services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opts =>
    {
      opts.TokenValidationParameters = new TokenValidationParameters
      {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
                  Encoding.UTF8.GetBytes(conf["TokenKey"]!)
              ),
        ValidateIssuer = false,
        ValidateAudience = false,
      };
    });
    return services;
  }
}