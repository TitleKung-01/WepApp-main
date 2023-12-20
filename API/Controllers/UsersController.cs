using API.Controllers;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

public class UsersController : BaseApiController
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;


    public UsersController(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
    {
        var users = await _userRepository.GetUsersAsync();
        return Ok(_mapper.Map<IEnumerable<MemberDto>>(users));
    }

    public async Task<ActionResult<MemberDto?>> GetUser(int id)
    {
        var user = await _userRepository.GetUserByIdAsync(id);
        return _mapper.Map<MemberDto>(user);
    }

    public async Task<ActionResult<MemberDto?>> GetUserByUserName(string username)
    {
        // var user = await _userRepository.GetUserByUserNameAsync(username);
        // return _mapper.Map<MemberDto>(user);
        return await _userRepository.GetMemberAsync(username);

    }
}