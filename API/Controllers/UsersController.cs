using API.Controllers;
using API.Data;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        // var users = await _userRepository.GetUsersAsync();
        // return Ok(_mapper.Map<IEnumerable<MemberDto>>(users));
        return Ok(await _userRepository.GetMembersAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MemberDto>> GetUser(int id) => await _userRepository.GetUserByIdAsync(id);

    [HttpGet("username/{username}")]
    public async Task<ActionResult<MemberDto>> GetUserByUserName(string username)
    {
        Console.WriteLine($"username: {username}");
        return await _userRepository.GetMemberAsync(username);
    }
}