using API.Entities;

namespace api;

public class Message
{
  public int Id { get; set; }
  public string? Content { get; set; }
  public DateTime? DateRead { get; set; }
  public DateTime DateSent { get; set; } = DateTime.UtcNow;
  public bool IsSenderDeleted { get; set; }
  public bool IsRecipientDeleted { get; set; }

  public int SenderId { get; set; }
  public int RecipientId { get; set; }
  public string? SenderUsername { get; set; }
  public string? RecipientUsername { get; set; }
  public AppUser? Sender { get; set; }
  public AppUser? Recipient { get; set; }
}