using api;
using API.Entities;

namespace API.Interfaces;

public interface IUserRepository
{
  void Update(AppUser user);
  Task<bool> SaveAllAsync();
  Task<AppUser> GetUserByIdAsync(int id);
  Task<AppUser> GetUserByUserNameAsync(string username);
  Task<IEnumerable<MemberDto>> GetUsersAsync();
  Task<PageList<MemberDto>> GetMembersAsync(UserParams userParams);
  Task<MemberDto> GetMemberAsync(string username);
}