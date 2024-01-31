namespace api;

public class LikeDto
{
  public int Id { get; set; }
  public int Age { get; set; }
  public string? UserName { get; set; }
  public string? Aka { get; set; }
  public string? City { get; set; }
  public string? Country { get; set; }
  public string? MainPhotoUrl { get; set; }
  public List<Photo>? Photos { get; set; }
}