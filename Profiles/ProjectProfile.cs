using AutoMapper;
using Lagalt_Backend.Models.Domain;
using Lagalt_Backend.Models.Dto.Projects;

namespace Lagalt_Backend.Profiles
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<ProjectPostDto, Project>();
            CreateMap<ProjectPutDto, Project>();
            CreateMap<Project, ProjectDto>()
                .ForMember(dto => dto.Owner, opt => opt
                .MapFrom(p => p.Owner.Id))
                .ForMember(dto => dto.Contributors, opt => opt
                .MapFrom(p => p.Contributors.Select(c => c.Id).ToList()))
                .ForMember(dto => dto.Images, opt => opt
                .MapFrom(p => p.Images.Select(c => c.Id).ToList()));

            
        }
    }
}
