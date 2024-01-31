using System.Text.Json;

namespace api;

public static class HttpExtensions
{
  public static void AddPaginationHeader(this HttpResponse response, PaginationHeader header)
  {
    var jsonOpts = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
    response.Headers.Add("Pagination", JsonSerializer.Serialize(header, jsonOpts));
    response.Headers.Add("Access-Control-Expose-Headers", "Pagination");
  }
}