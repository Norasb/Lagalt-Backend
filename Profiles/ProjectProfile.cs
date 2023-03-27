using AutoMapper;
using Lagalt_Backend.Models.Domain;
using Lagalt_Backend.Models.Dto.Projects;

namespace Lagalt_Backend.Profiles
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<ProjectPostDto, Project>()
                .ForMember(dest => dest.Skills, opt => opt.Ignore());
            CreateMap<ProjectPutDto, Project>();
            CreateMap<Project, ProjectDto>()
                .ForMember(dto => dto.Owner, opt => opt
                .MapFrom(p => p.Owner.Id))
                .ForMember(dto => dto.Contributors, opt => opt
                .MapFrom(p => p.Contributors.Select(c => c.Id).ToList()))
                .ForMember(dto => dto.Images, opt => opt
                .MapFrom(p => p.Images.Select(i => i.Id).ToList()))
                .ForMember(dto => dto.Tags, opt => opt
                .MapFrom(p => p.Tags.Select(t => t.Id).ToList()))
                .ForMember(dto => dto.Skills, opt => opt
                .MapFrom(p => p.Skills.Select(s => s.Id).ToList()));

            
        }
    }
}
