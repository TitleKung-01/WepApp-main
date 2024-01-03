using API.Entities;
using AutoMapper;

namespace api;

public class AutoMapperUserProfiles : Profile
{
  public AutoMapperUserProfiles()
  {
    CreateMap<AppUser, MemberDto>()
            .ForMember(
                user => user.MainPhotoUrl,
                opt => opt.MapFrom(
                    user => user.Photos.FirstOrDefault(photo => photo.IsMain).Url
                    )
                )
            .ForMember(
                user => user.Age,
                opt => opt.MapFrom(
                    user => user.BirthDate.CalculateAge()
                    )
                );
    CreateMap<Photo, PhotoDto>();
    CreateMap<MemberUpdateDto, AppUser>();
  }
}