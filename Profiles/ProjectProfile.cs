﻿using AutoMapper;
using Lagalt_Backend.Models.Domain;
using Lagalt_Backend.Models.Dto.Projects;

namespace Lagalt_Backend.Profiles
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<ProjectPostDto, Project>()
                .ForMember(dest => dest.Skills, opt => opt.Ignore())
                .ForMember(dest => dest.Tags, opt => opt.Ignore());
            CreateMap<ProjectPutDto, Project>();
            CreateMap<Project, ProjectDto>()
                .ForMember(dto => dto.Owner, opt => opt
                .MapFrom(p => p.Owner.UserName))
                .ForMember(dto => dto.Images, opt => opt
                .MapFrom(p => p.Images.Select(c => c.Url).ToList()))
                .ForMember(dto => dto.Tags, opt => opt
                .MapFrom(p => p.Tags.Select(t => t.Id).ToList()))
                .ForMember(dto => dto.Skills, opt => opt
                .MapFrom(p => p.Skills.Select(s => s.Id).ToList()));

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
