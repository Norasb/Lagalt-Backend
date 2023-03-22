using AutoMapper;
using Lagalt_Backend.Models.Domain;
using Lagalt_Backend.Models.Dto.Tag;

namespace Lagalt_Backend.Profiles
{
    public class TagProfile : Profile
    {
        public TagProfile()
        {
            CreateMap<TagPostDto, Tag>();
            CreateMap<TagPutDto, Tag>();
            CreateMap<Tag, TagDto>();
        }
    }
}
