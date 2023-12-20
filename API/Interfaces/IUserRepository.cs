using api;
using API.Entities;

namespace API.Interfaces;

public interface IUserRepository
{
  void Update(AppUser user);
  Task<bool> SaveAllAsync();
  Task<MemberDto> GetUserByIdAsync(int id);
  Task<MemberDto> GetUserByUserNameAsync(string username);
  Task<IEnumerable<MemberDto>> GetUsersAsync();
  Task<IEnumerable<MemberDto>> GetMembersAsync();
  Task<MemberDto> GetMemberAsync(string username);
}