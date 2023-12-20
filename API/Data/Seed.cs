using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using API.Data;
using API.Entities;
using Microsoft.EntityFrameworkCore;

public class Seed
{
  public static async Task SeedUsers(DataContext dataContext)
  {
    if (await dataContext.Users.AnyAsync())
      return;
    var userSeedData = await File.ReadAllTextAsync("Data/UserSeedData.json");
    var opt = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    var users = JsonSerializer.Deserialize<List<AppUser>>(userSeedData, opt);
    foreach (var user in users)
    {
      using var hmacSHA256 = new HMACSHA256();
      user.UserName = user.UserName.ToLower();
      user.PasswordHash = hmacSHA256.ComputeHash(Encoding.UTF8.GetBytes("P@ssw0rd"));
      user.PasswordSalt = hmacSHA256.Key;

      dataContext.Users.Add(user);
    }
    await dataContext.SaveChangesAsync();
  }
}