using API.Entities;
using API.Extensions;
using AutoMapper;

#nullable disable
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
                );
        CreateMap<Photo, PhotoDto>();
        CreateMap<AppUser, MemberDto>(
            ).ForMember(
                user => user.Age,
                opt => opt.MapFrom(user => user.BirthDate.CalculateAge()
        ));
    }
}