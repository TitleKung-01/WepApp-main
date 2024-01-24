using System;

namespace API.DTOs;

public class UserDto
{
    public string? Username { get; set; }
    public string? Token { get; set; }
    public string? PhotoUrl { get; set; }
    public string? Aka { get; set; }
    public string? Gender { get; set; }
}
