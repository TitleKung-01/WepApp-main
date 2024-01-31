using api.Controllers;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api;

public class ErrorController : BaseApiController
{
  private readonly DataContext _dataContext;

  public ErrorController(DataContext dataContext)
  {
    _dataContext = dataContext;
  }

  [Authorize]
  [HttpGet("auth")]
  public ActionResult<string> GetSecret()
  {
    return "xxx";
  }

  [HttpGet("not-found")]
  public ActionResult<AppUser> GetNotFound()
  {
    var user = _dataContext.Users.Find(-1);
    if (user is null) return NotFound();
    return user;
  }

  [HttpGet("server-error")]
  public ActionResult<string> GetServerError()
  {
    try
    {
      var user = _dataContext.Users.Find(-1);
      var stringUser = user.ToString();
      return stringUser;
    }
    catch (Exception e)
    {
      throw new Exception(e.Message);
    }
  }

  [HttpGet("bad-request")]
  public ActionResult<string> GetBadRequest()
  {
    return BadRequest("illegal request");
  }
}