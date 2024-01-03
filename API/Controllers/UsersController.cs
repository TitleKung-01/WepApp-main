using System.Security.Claims;
using API.Data;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers;

[Authorize]
public class UsersController : BaseApiController
{
  private readonly IUserRepository _userRepository;
  private readonly IMapper _mapper;
  private readonly IImageService _imageService;

  public UsersController(IUserRepository userRepository, IMapper mapper, IImageService imageService)
  {
    _userRepository = userRepository;
    _mapper = mapper;
    _imageService = imageService;

  }

  private async Task<AppUser?> _GetUser()
  {
    var username = User.GetUsername();
    if (username is null) return null;
    return await _userRepository.GetUserByUserNameAsync(username);
  }

  [HttpGet]
  public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
  {
    return Ok(await _userRepository.GetMembersAsync());
  }

  [HttpGet("{id}")]
  public async Task<ActionResult<AppUser>> GetUser(int id)
  {
    return await _userRepository.GetUserByIdAsync(id);
  }

  [HttpGet("username/{username}")]
  public async Task<ActionResult<AppUser>> GetUserByUserName(string username)
  {
    return await _userRepository.GetUserByUserNameAsync(username);
  }

  [HttpPut]
  public async Task<ActionResult> UpdateUserProfile(MemberUpdateDto memberUpdateDto)
  {

    var user = await _GetUser();
    if (user is null) return NotFound();

    var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value; //มาจาก TokenService.cs -> CreateToken
    if (username is null) return Unauthorized();

    var AppUser = await _userRepository.GetUserByUserNameAsync(username);
    if (user is null) return NotFound();

    _mapper.Map(memberUpdateDto, user);
    if (await _userRepository.SaveAllAsync()) return NoContent();

    return BadRequest("Failed to update user profile!");
  }

  [HttpPost("add-image")]
  public async Task<ActionResult<PhotoDto>> AddPhoto(IFormFile file)
  {
    var user = await _GetUser();
    if (user is null) return NotFound();

    var result = await _imageService.AddImageAsync(file);
    if (result.Error is not null) return BadRequest(result.Error.Message);

    var photo = new Photo
    {
      Url = result.SecureUrl.AbsoluteUri,
      PublicId = result.PublicId
    };
    if (user.Photos.Count == 0) photo.IsMain = true;

    user.Photos.Add(photo);
    // if (await _userRepository.SaveAllAsync()) return _mapper.Map<PhotoDto>(photo);
    if (await _userRepository.SaveAllAsync())
    {
      return CreatedAtAction( //status 201
          nameof(GetUserByUserName),
          new { username = user.UserName },
          _mapper.Map<PhotoDto>(photo)
      );
    }
    return BadRequest("Something has gone wrong!");
  }

}