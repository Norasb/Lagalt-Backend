using AutoMapper;
using Lagalt_Backend.Models.Domain;
using Lagalt_Backend.Models.Dto.Message;

namespace Lagalt_Backend.Profiles
{
    public class MessageProfile : Profile
    {
        public MessageProfile()
        {
            CreateMap<MessagePostDto, Message>()
                .ForMember(m => m.UserId, opt => opt
                .MapFrom(dto => dto.UserId));
            CreateMap<MessagePutDto, Message>();
            CreateMap<Message, MessageDto>()
                .ForMember(dto => dto.User, opt => opt
                .MapFrom(m => m.User.Id));
        }
    }
}
