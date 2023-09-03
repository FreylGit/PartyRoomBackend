using AutoMapper;
using PartyRoom.Core.Constants;
using PartyRoom.Core.DTOs.Tag;
using PartyRoom.Core.DTOs.User;
using PartyRoom.Core.Entities;

namespace PartyRoom.WebAPI.MappingProfiles
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            // CreateMap<ApplicationUser, UserLoginDTO>();
            CreateMap<UserLoginDTO, ApplicationUser>();
            CreateMap<UserRegistrationDTO, ApplicationUser>();
            CreateMap<UserDTO, ApplicationUser>();
            CreateMap<ApplicationUser, UserDTO>();

            CreateMap<UserProfile, UserProfileDTO>()
                .ForMember(dest => dest.ImagePath,opt=>opt.MapFrom(src=> ApplicationConstants.URL_IMAGE+ src.ImagePath));
            CreateMap<ApplicationUser, UserDTO>()
                .ForMember(dest=>dest.FirtsName,opt=>opt.MapFrom(src=>src.FirstName))
                .ForMember(dest => dest.Details, opt => opt.MapFrom(src => src.UserProfile));

            CreateMap<ApplicationUser,UserPublicDTO>()
                .ForMember(dest => dest.FirtsName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.Details, opt => opt.MapFrom(src => src.UserProfile));

            CreateMap<TagCreateDTO, Tag>();
            CreateMap<Tag, TagDTO>();

        }
    }
}
