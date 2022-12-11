using API.DTOs;
using API.Entities;
using API.Extentions;
using AutoMapper;

namespace API.Helpers;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<AppUser,MembersDto>()
        .ForMember(dest => dest.PhotoUrl,
        options => options.MapFrom(source => source.Photos.FirstOrDefault(x => x.IsMain).Url))
        .ForMember(dest =>  dest.Age,
        options => options.MapFrom(source => source.DateOfBirth.CalculateAge()));
        
        CreateMap<Photo,PhotoDto>();
    }    
}
