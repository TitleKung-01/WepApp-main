using System.ComponentModel.DataAnnotations;

namespace API.DTOs;

public class RegisterDto
{

  [Required(ErrorMessage = "Username is required")]
  [MinLength(3, ErrorMessage = "Username must be at least 3 characters")]
  public string Username { get; set; }

  [Required(ErrorMessage = "Password is required")]
  [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
  public string Password { get; set; }
}