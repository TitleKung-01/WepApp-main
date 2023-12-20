using System;
using System.ComponentModel.DataAnnotations;
namespace API.DTOs;

public record RegisterDto
// public class RegisterDto
{
    [Required(ErrorMessage = "Please enter a username \n Please enter a password")]
    [MinLength(3, ErrorMessage = "Please enter a username at least 3 characters")]
    public string? Username { get; set; }


    [MinLength(3, ErrorMessage = "Please enter a password at least 3 characters")]
    public string? Password { get; set; }
}
