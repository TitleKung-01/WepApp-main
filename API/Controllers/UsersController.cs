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

  public UsersController(IUserRepository userRepository, IMapper mapper)
  {
    _userRepository = userRepository;
    _mapper = mapper;
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
    var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value; //มาจาก TokenService.cs -> CreateToken
    if (username is null) return Unauthorized();

    var user = await _userRepository.GetUserByUserNameAsync(username);
    if (user is null) return NotFound();

    _mapper.Map(memberUpdateDto, user);
    if (await _userRepository.SaveAllAsync()) return NoContent();

    return BadRequest("Failed to update user profile!");
  }
}