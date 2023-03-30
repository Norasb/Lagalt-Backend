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

            CreateMap<ApplicationPostDTO, Application>()
                .ForMember(a => a.UserId, opt => opt
                .MapFrom(dto => dto.UserId))
                .ForMember(a => a.ProjectId, opt => opt
                .MapFrom(dto => dto.ProjectId));

            CreateMap<Application, ApplicationDTO>()
                .ForMember(dto => dto.ProjectTitle, opt => opt
                .MapFrom(a => a.Project.Title))
                .ForMember(dto => dto.UserName, opt => opt
                .MapFrom(a => a.User.UserName));

            CreateMap<Application, ApplicationStatusDTO>()
                .ForMember(dto => dto.Id, opt => opt
                .MapFrom(a => a.Id));

            CreateMap<Application, ApplicationsInUserDto>()
                .ForMember(dto => dto.ProjectId, opt => opt
                .MapFrom(a => a.ProjectId));
        }
    }
}
