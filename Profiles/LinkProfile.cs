using AutoMapper;
using Lagalt_Backend.Models.Domain;
using Lagalt_Backend.Models.Dto.Link;

namespace Lagalt_Backend.Profiles
{
    /// <summary>
    /// Mappings for Link entity to Post, Put and Read DTOs.
    /// </summary>
    public class LinkProfile : Profile
    {
        public LinkProfile()
        {
            CreateMap<LinkPostDto, Link>();
            CreateMap<LinkPutDto, Link>();
            CreateMap<Link, LinkDto>();
        }
        

    }
}
