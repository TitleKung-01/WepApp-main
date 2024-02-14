
using System.Text.Json.Serialization;
using api;
using Microsoft.AspNetCore.Identity;

namespace API.Entities;

public class AppUser : IdentityUser<int>
{

  // public int Id { get; set; }

  // public string UserName { get; set; }
  // public byte[] PasswordHash { get; set; }
  // public byte[] PasswordSalt { get; set; }
  public ICollection<AppUserRole> UserRoles { get; set; }


  public DateOnly BirthDate { get; set; }
  public string Aka { get; set; }
  public string Gender { get; set; }
  public string Introduction { get; set; }
  public string LookingFor { get; set; }
  public string Interests { get; set; }
  public string City { get; set; }
  public string Country { get; set; }

  [JsonIgnore]
  public List<Photo> Photos { get; set; } = new();

  public DateTime Created { get; set; } = DateTime.UtcNow;
  public DateTime LastActive { get; set; } = DateTime.UtcNow;

  public List<UserLike>? LikedByUsers { get; set; }
  public List<UserLike>? LikedUsers { get; set; }

  public List<Message>? MessagesSent { get; set; }
  public List<Message>? MessagesReceived { get; set; }
}