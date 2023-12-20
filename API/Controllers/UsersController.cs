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

  public UsersController(IUserRepository userRepository)
  {
    _userRepository = userRepository;
  }

  [HttpGet]
  public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
  {
    return Ok(await _userRepository.GetMembersAsync());
  }

  [HttpGet("{id}")]
  public async Task<ActionResult<MemberDto>> GetUser(int id) => await _userRepository.GetUserByIdAsync(id);

  [HttpGet("username/{username}")]
  public async Task<ActionResult<MemberDto>> GetUserByUserName(string username)
  {
    return await _userRepository.GetMemberAsync(username);
  }
}