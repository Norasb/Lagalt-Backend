using AutoMapper;
using Lagalt_Backend.Models.Domain;
using Lagalt_Backend.Models.Dto.Tag;

namespace Lagalt_Backend.Profiles
{
    /// <summary>
    /// Mappings for Tag entity to Post, Put and Read DTOs.
    /// </summary>
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
