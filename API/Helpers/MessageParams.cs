namespace api;

public class MessageParams : PaginationParams
{
  public string? Username { get; set; }
  public string Label { get; set; } = "Unread";
}