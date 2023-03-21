using AutoMapper;
using Lagalt_Backend.Models.Domain;
using Lagalt_Backend.Models.DTOs.Users;
using Microsoft.OpenApi.Writers;

namespace Lagalt_Backend.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile() 
        {
            CreateMap<UserPutDTO, User>();
            CreateMap<UserPostDTO, User>();
            CreateMap<User, UserDTO>()
                .ForMember(dto => dto.Messages, opt => opt.MapFrom(u => u.Messages!.Select(m => m.Id).ToList()))
                .ForMember(dto => dto.Skills, opt => opt.MapFrom(u => u.Skills!.Select(s => s.Id).ToList()))
                .ForMember(dto => dto.ContributedProjects, opt => opt.MapFrom(u => u.ContributedProjects!.Select(cp => cp.Id).ToList()))
                .ForMember(dto => dto.OwnedProjects, opt => opt.MapFrom(u => u.OwnedProjects!.Select(op => op.Id).ToList()))
                .ForMember(dto => dto.Applications, opt => opt.MapFrom(u => u.Applications!.Select(a => a.Id).ToList()));

        }

    }
}
