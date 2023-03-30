using AutoMapper;
using Lagalt_Backend.Models.Domain;
using Lagalt_Backend.Models.Dto.Message;

namespace Lagalt_Backend.Profiles
{
    /// <summary>
    /// Mappings for Message entity to Post, Put and Read DTOs.
    /// </summary>
    public class MessageProfile : Profile
    {
        public MessageProfile()
        {
            CreateMap<MessagePostDto, Message>();
            CreateMap<MessagePutDto, Message>();
            CreateMap<Message, MessageDto>()
                .ForMember(dto => dto.User, opt => opt
                .MapFrom(m => m.User.Id));
        }
    }
}
