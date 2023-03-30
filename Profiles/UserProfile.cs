using AutoMapper;
using Lagalt_Backend.Models;
using Lagalt_Backend.Models.Domain;
using Lagalt_Backend.Models.Dto.User;

namespace Lagalt_Backend.Profiles
{
    /// <summary>
    /// Mappings for User entity to Post, Put and Read DTOs.
    /// </summary>
    public class UserProfile : Profile
    {
        private readonly IMapper _mapper;

        public UserProfile(IMapper mapper)
        {
            _mapper = mapper;
        }

        public UserProfile() 
        {
            CreateMap<UserPutDTO, User>()
                .ForMember(p => p.Skills, opt => opt
                .MapFrom(dto => dto.Skills.Select(s => new Skill { Name = s })));


            CreateMap<UserPostDTO, User>();
                
            CreateMap<User, UserDTO>()
                .ForMember(dto => dto.Skills, opt => opt
                .MapFrom(u => u.Skills.Select(s => s.Name).ToList()));
        }
    }
}
