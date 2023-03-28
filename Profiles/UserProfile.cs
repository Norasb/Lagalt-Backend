using AutoMapper;
using Lagalt_Backend.Models;
using Lagalt_Backend.Models.Domain;
using Lagalt_Backend.Models.Dto.User;

namespace Lagalt_Backend.Profiles
{
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
                .ForMember(dest => dest.Skills, opt => opt.MapFrom(src => src.Skills.Select(s => _mapper.Map<Skill>(s))));

            CreateMap<UserPostDTO, User>();
                
            CreateMap<User, UserDTO>()
                .ForMember(dto => dto.Skills, opt => opt
                .MapFrom(u => u.Skills.Select(s => s.Name).ToList()));


            //CreateMap<string, Skill>()
            //    .ConstructUsing(name => new Skill { Name = name });

            //CreateMap<UserPutDTO, User>()
            //    .ForMember(dest => dest.Skills, opt => opt
            //    .MapFrom(src => src.Skills.Select(skillName => _context.Skills.FirstOrDefault(skill => skill.Name == skillName))));

        }
    }
}
