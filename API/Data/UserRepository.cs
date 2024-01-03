using api;
using API.Data;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

public class UserRepository : IUserRepository
{
  private readonly DataContext _dataContext;
  private readonly IMapper _mapper;

  public UserRepository(DataContext dataContext, IMapper mapper)
  {
    _dataContext = dataContext;
    _mapper = mapper;
  }

  public async Task<IEnumerable<MemberDto>> GetMembersAsync()
  {
    return await _dataContext.Users.ProjectTo<MemberDto>(_mapper.ConfigurationProvider).ToListAsync();
  }


  // public async Task<MemberDto> GetUserByIdAsync(int id)
  // {
  //   return await _dataContext.Users
  //       .Where(user => user.Id == id)
  //       .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
  //       .SingleOrDefaultAsync();
  // }

  // public async Task<MemberDto> GetUserByUserNameAsync(string username)
  // {
  //   return await _dataContext.Users
  //       .Where(user => user.UserName == username)
  //       .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
  //       .SingleOrDefaultAsync();
  // }

  public async Task<IEnumerable<MemberDto>> GetUsersAsync()
  {
    return await _dataContext.Users
        .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
        .ToListAsync();
  }

  public async Task<bool> SaveAllAsync() => await _dataContext.SaveChangesAsync() > 0;

  public async Task<MemberDto> GetMemberAsync(string username)
  {
    return await _dataContext.Users
        .Where(user => user.UserName == username)
        .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
        .SingleOrDefaultAsync();
  }

  public void Update(AppUser user)
  {
    _dataContext.Entry(user).State = EntityState.Modified;
  }

  public async Task<AppUser> GetUserByIdAsync(int id)
  {
    return await _dataContext.Users.FindAsync(id);
  }

  public async Task<AppUser> GetUserByUserNameAsync(string username)
  {
    return await _dataContext.Users
        .Include(user => user.Photos)
        .SingleOrDefaultAsync(user => user.UserName == username);
  }
}