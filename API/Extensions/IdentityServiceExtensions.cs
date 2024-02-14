using System.Text;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace API.Extensions;

public static class IdentityServiceExtensions
{
  public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration conf)
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
    services.AddIdentityCore<AppUser>(opts =>
        {
          opts.Password.RequiredLength = 8;
          // opts.User.RequireUniqueEmail = false;
        })
        .AddRoles<AppRole>()
        .AddRoleManager<RoleManager<AppRole>>()
        .AddEntityFrameworkStores<DataContext>();
    services.AddAuthorization(opts =>
    {
      opts.AddPolicy("AdminRole", policy => policy.RequireRole("Administrator"));
      opts.AddPolicy("ModeratePhotoRole", policy => policy.RequireRole("Administrator", "Moderator"));
    });
    return services;
  }
}