using System;
using api.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class AdminController : BaseApiController
{
    [Authorize(Policy = "AdminRole")]
    [HttpGet("users-with-roles")]
    public ActionResult GetUsersRoles()
    {
        return Ok("Hello Admin");
    }

    [Authorize(Policy = "ModeratePhotoRole")]
    [HttpGet("photos-to-moderate")]
    public ActionResult GetPhotosForModeration()
    {
        return Ok("Hello Admin/Moderator");
    }


}
