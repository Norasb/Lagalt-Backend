using AutoMapper;
using Lagalt_Backend.Models.Domain;
using Lagalt_Backend.Models.Dto.Link;

namespace Lagalt_Backend.Profiles
{
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
