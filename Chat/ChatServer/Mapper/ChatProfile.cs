using AutoMapper;
using ChatServer.Dto;
using DAL.DbModels;

namespace ChatServer.Mapper
{
    public class ChatProfile : Profile
    {
        public ChatProfile()
        {
            CreateMap<ChatUser, ChatUserDto>();
            CreateMap<ChatRoom, ChatRoomDto>();
            CreateMap<ChatMessage, ChatMessageDto>();
            CreateMap<ChatRoomChatUser, ChatRoomChatUserDto>();

            CreateMap<ChatUserDto, ChatUser>();
            CreateMap<ChatRoomDto, ChatRoom>();
            CreateMap<ChatMessageDto, ChatMessage>();
            CreateMap<ChatRoomChatUserDto, ChatRoomChatUser>();
        }
    }
}
