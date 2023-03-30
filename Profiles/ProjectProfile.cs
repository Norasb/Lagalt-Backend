using AutoMapper;
using Lagalt_Backend.Models.Domain;
using Lagalt_Backend.Models.Dto.Projects;

namespace Lagalt_Backend.Profiles
{
    /// <summary>
    /// Mappings for Project entity to Post, Put and Read DTOs.
    /// </summary>
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<ProjectPostDto, Project>();
            CreateMap<ProjectPutDto, Project>();
            CreateMap<Project, ProjectDto>()
                .ForMember(dto => dto.Owner, opt => opt
                .MapFrom(p => p.Owner.Id))
                .ForMember(dto => dto.Tags, opt => opt
                .MapFrom(p => p.Tags.Select(t => t.Name).ToList()))
                .ForMember(dto => dto.Skills, opt => opt
                .MapFrom(p => p.Skills.Select(s => s.Name).ToList()))
                .ForMember(dto => dto.ImageUrls, opt => opt
                .MapFrom(p => p.Images.Select(c => c.Url).ToList()));

            CreateMap<Project, ProjectOneDto>()
                .ForMember(dto => dto.Owner, opt => opt
                .MapFrom(p => p.Owner.UserName))
                .ForMember(dto => dto.Contributors, opt => opt
                .MapFrom(p => p.Contributors.Select(c => c.UserName).ToList()))
                .ForMember(dto => dto.ImageUrls, opt => opt
                .MapFrom(p => p.Images.Select(c => c.Url).ToList()))
                .ForMember(dto => dto.Tags, opt => opt
                .MapFrom(p => p.Tags.Select(t => t.Name).ToList()))
                .ForMember(dto => dto.Skills, opt => opt
                .MapFrom(p => p.Skills.Select(s => s.Name).ToList()))
                .ForMember(dto => dto.LinkUrls, opt => opt
                .MapFrom(p => p.Links.Select(l => l.URL).ToList()));
        }
    }
}
