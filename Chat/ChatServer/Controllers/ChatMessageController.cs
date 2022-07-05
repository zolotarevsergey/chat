using AutoMapper;
using BLL;
using BLL.Exceptions;
using ChatServer.Dto;
using DAL.DbModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChatServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChatMessageController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IChatMessageManager _chatMessageManager;
        private readonly IChatRoomManager _chatRoomManager;
        private readonly IChatUserManager _chatUserManager;

        public ChatMessageController(IMapper mapper, IChatMessageManager chatMessageManager,
            IChatRoomManager chatRoomManager, IChatUserManager chatUserManager)
        {
            _mapper = mapper;
            _chatMessageManager = chatMessageManager;
            _chatRoomManager = chatRoomManager;
            _chatUserManager = chatUserManager;
        }

        [HttpGet]
        public async Task<List<ChatMessageDto>> GetMessages()
        {
            var result = await _chatMessageManager.GetChatMessagesAsync();
            return _mapper.Map<List<ChatMessageDto>>(result);
        }

        [HttpPost]
        public async Task<ActionResult<ChatMessageDto>> PostMessage(ChatMessageDto message)
        {
            try
            {
                var result = await _chatMessageManager.CreateChatMessageAsync(_mapper.Map<ChatMessage>(message));
                return _mapper.Map<ChatMessageDto>(result);
            }
            catch(CustomException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
