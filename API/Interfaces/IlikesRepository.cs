using API.Entities;

namespace api;

public interface IlikesRepository
{
  Task<UserLike> GetUserLike(int sourceUserId, int likedUserId);
  Task<PageList<LikeDto>> GetUserLikes(LikesParams likesParams);

  Task<AppUser> GetUser(int userId);

  // Task<IEnumerable<LikeDto>> GetUserLikes(string predicate, int userId);
}