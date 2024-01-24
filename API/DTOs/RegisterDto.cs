using System;
using System.ComponentModel.DataAnnotations;
namespace API.DTOs;

public record RegisterDto
// public class RegisterDto
{
    [Required(ErrorMessage = "Please enter a username")]
    [MinLength(3, ErrorMessage = "Please enter a username at least 3 characters")]
    public string? Username { get; set; }

    [MinLength(3, ErrorMessage = "Please enter a password at least 3 characters")]
    public string? Password { get; set; }
    [Required] public string? Aka { get; set; }
    [Required] public string? Gender { get; set; }
    [Required] public DateOnly? BirthDate { get; set; }
    [Required] public string? City { get; set; }
    [Required] public string? Country { get; set; }
}
