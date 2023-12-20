namespace api.Errors;

public class ApiException
{
  public int StatusCode { get; set; }
  public string Messages { get; set; }
  public string Details { get; set; }

  public ApiException(int statusCode, string messages, string details)
  {
    StatusCode = statusCode;
    Messages = messages;
    Details = details;
  }
}