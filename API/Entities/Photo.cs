using System.ComponentModel.DataAnnotations.Schema;
using API.Entities;

namespace api;

[Table("Photos")]
public class Photo
{
  public int AppUserID { get; set; }
  public AppUser? AppUser { get; set; }
  public int Id { get; set; }
  public string? Url { get; set; }
  public string? PublicId { get; set; }
  public bool IsMain { get; set; }
}