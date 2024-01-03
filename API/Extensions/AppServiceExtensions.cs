using api;
using API.Data;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions;

public static class AppServiceExtensions
{
  public static IServiceCollection AddAppServices(this IServiceCollection services, IConfiguration conf)
  {
    services.AddDbContext<DataContext>(opt =>
    {
      opt.UseSqlite(conf.GetConnectionString("SqliteConnection"));
    });
    services.AddCors();
    services.AddScoped<ITokenService, TokenService>();
    services.AddScoped<IUserRepository, UserRepository>();
    services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    services.Configure<CloudinarySettings>(conf.GetSection("CloudinarySettings"));
    services.AddScoped<IImageService, ImageService>();

    return services;
  }
}