using AutoMapper;
using Lagalt_Backend.Models.Domain;
using Lagalt_Backend.Models.Dto.User;

namespace Lagalt_Backend.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile() 
        {
            CreateMap<UserPutDTO, User>();
            CreateMap<UserPostDTO, User>();
            CreateMap<User, UserDTO>()
                .ForMember(dto => dto.Skills, opt => opt
                .MapFrom(u => u.Skills.Select(s => s.Name).ToList()));
        }
    }
}
