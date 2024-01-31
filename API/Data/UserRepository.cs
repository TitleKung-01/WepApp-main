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

  public async Task<PageList<MemberDto>> GetMembersAsync(UserParams userParams)
  {
    var query = _dataContext.Users.AsQueryable();
    query = query.Where(user => user.UserName != userParams.CurrentUserName);
    if (userParams.Gender != "non-binary")
      query = query.Where(user => user.Gender == userParams.Gender);
    query.AsNoTracking();

    var minBirthDate = DateOnly.FromDateTime(DateTime.Today.AddYears(-userParams.MaxAge - 1));
    var maxBirthDate = DateOnly.FromDateTime(DateTime.Today.AddYears(-userParams.MinAge));
    query = query.Where(user => user.BirthDate >= minBirthDate && user.BirthDate <= maxBirthDate);

    query = userParams.OrderBy switch
    {
      "created" => query.OrderByDescending(user => user.Created),
      _ => query.OrderByDescending(user => user.LastActive),
    };
    query.AsNoTracking();

    return await PageList<MemberDto>.CreateAsync(
        query.ProjectTo<MemberDto>(_mapper.ConfigurationProvider),
        userParams.PageNumber,
        userParams.PageSize);

  }

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