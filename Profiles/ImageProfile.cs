using AutoMapper;
using Lagalt_Backend.Models.Domain;
using Lagalt_Backend.Models.Dto.Image;

namespace Lagalt_Backend.Profiles
{
    /// <summary>
    /// Mappings for Image entity to Post, Put and Read DTOs.
    /// </summary>
    public class ImageProfile : Profile
    {
        public ImageProfile() 
        {
            CreateMap<ImagePostDTO, Image>();
            CreateMap<ImagePutDTO, Image>();
            CreateMap<Image, ImageDTO>();
        }
    }
}
