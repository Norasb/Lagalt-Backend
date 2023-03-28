using AutoMapper;
using Lagalt_Backend.Models.Domain;
using Lagalt_Backend.Models.Dto.Skill;

namespace Lagalt_Backend.Profiles
{
    public class SkillProfile : Profile
    {

        public SkillProfile()
        {
            CreateMap<SkillPostDto, Skill>();
            CreateMap<SkillPutDto, Skill>();
            CreateMap<Skill, SkillDto>();
            CreateMap<SkillUserDto, Skill>();
        }
        
    }
}
