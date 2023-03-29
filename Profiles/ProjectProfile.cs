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

            CreateMap<ProjectPutDto, Project>()
                .ForMember(p => p.Tags, opt => opt
                .MapFrom(dto => dto.Tags.Select(tag => new Tag { Name = tag })))
                .ForMember(p => p.Skills, opt => opt
                .MapFrom(dto => dto.Skills.Select(skill => new Skill { Name = skill })))
                .ForMember(p => p.Images, opt => opt
                .MapFrom(dto => dto.ImageUrls.Select(url => new Image { Url = url })))
                .ForMember(p => p.Links, opt => opt
                .MapFrom(dto => dto.Links.Select(url => new Link { URL = url })))
                .ForMember(p => p.Contributors, opt => opt
                .MapFrom(dto => dto.UsersContributed.Select(id => new User { Id = id })));

            //CreateMap<ProjectPutDto, Project>()
            //    .ForMember(p => p.Tags, opt => opt
            //    .MapFrom(dto => dto.Tags))
            //    .ForMember(p => p.Skills, opt => opt
            //    .MapFrom(dto => dto.Skills))
            //    .ForMember(p => p.Images, opt => opt
            //    .MapFrom(dto => dto.ImageUrls))
            //    .ForMember(p => p.Links, opt => opt
            //    .MapFrom(dto => dto.Links))
            //    .ForMember(p => p.Contributors, opt => opt
            //    .MapFrom(dto => dto.UsersContributed));


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
