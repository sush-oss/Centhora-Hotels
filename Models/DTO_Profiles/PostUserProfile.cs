using AutoMapper;
using Centhora_Hotels.Models;
using Centhora_Hotels.Models.DTO;

namespace Centhora_Hotels.DTO_Profiles
{
    public class PostUserProfile : Profile
    {
        public PostUserProfile() 
        {
            CreateMap<User, PostUserDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.UserEmail, opt => opt.MapFrom(src => src.UserEmail))
                .ForMember(dest => dest.UserPassword, opt => opt.MapFrom(src => src.UserPassword));
                //.ForMember(dest => dest.UserImageURL, opt => opt.MapFrom(src => src.UserImageURL));

            CreateMap<PostUserDto, User>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.UserEmail, opt => opt.MapFrom(src => src.UserEmail))
                .ForMember(dest => dest.UserPassword, opt => opt.MapFrom(src => src.UserPassword));
                //.ForMember(dest => dest.UserImageURL, opt => opt.MapFrom(src => src.UserImageURL));
        }
    }
}
