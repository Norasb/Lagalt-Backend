using AutoMapper;
using Lagalt_Backend.Models.Domain;
using Lagalt_Backend.Models.Dto.Application;

namespace Lagalt_Backend.Profiles
{
    public class ApplicationProfile: Profile
    {
        public ApplicationProfile()
        {
            CreateMap<ApplicationPutDTO, Application>();
            CreateMap<ApplicationPostDTO, Application>();
            CreateMap<Application, ApplicationDTO>();
        }
    }
}
