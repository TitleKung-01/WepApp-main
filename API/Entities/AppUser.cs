namespace API.Entities;
using API.Extensions;

public class AppUser
{
    public int Id { get; set; }
    public string? UserName { get; set; }

    public  byte[]? PasswordHash { get; set; }
    public  byte[]? PasswordSalt { get; set; }

    public DateOnly BirthDate { get; set; }
    public string? Aka { get; set; }
    public string? Gender { get; set; }
    public string? Introduction { get; set; }
    public string? LookingFor { get; set; }
    public string? Interests { get; set; }
    public string? City { get; set; }
    public string? Country { get; set; }
    public List<Photo> Photos { get; set; } = new();
    public DateTime Created { get; set; } = DateTime.UtcNow;
    public DateTime LastActive { get; set; } = DateTime.UtcNow;

    // public int Age
    // {
    //     get
    //     {
    //         return BirthDate.CalculateAge(); //using API.Extensions
    //     }
    // }

}