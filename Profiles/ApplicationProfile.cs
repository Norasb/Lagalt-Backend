using AutoMapper;
using Lagalt_Backend.Models.Domain;
using Lagalt_Backend.Models.Dto.Application;

namespace Lagalt_Backend.Profiles
{
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            CreateMap<ApplicationPutDTO, Application>();
            CreateMap<ApplicationPostDTO, Application>();
            CreateMap<Application, ApplicationDTO>();
            CreateMap<Application, ApplicationStatusDTO>()
                .ForMember(dto => dto.UserName, opt => opt
                .MapFrom(a => a.User.UserName))
                .ForMember(dto => dto.ProjectTitle, opt => opt
                .MapFrom(a => a.Project.Title));
            CreateMap<Application, ApplicationsInUserDto>()
                .ForMember(dto => dto.ProjectId, opt => opt
                .MapFrom(a => a.ProjectId));
        }
    }
}
