namespace api;

public static class DateTimeExtensions
{
  public static int CalculateAge(this DateOnly birthdate)
  {
    var today = DateOnly.FromDateTime(DateTime.UtcNow);
    var age = today.Year - birthdate.Year;
    if (birthdate > today.AddYears(-age)) age--;
    return age;
  }
}
