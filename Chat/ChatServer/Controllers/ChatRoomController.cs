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
    public class ChatRoomController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IChatRoomManager _chatRoomManager;

        public ChatRoomController(IChatRoomManager chatRoomManager, IMapper mapper)
        {
            _chatRoomManager = chatRoomManager;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<List<ChatRoomDto>> GetRooms()
        {
            var result = await _chatRoomManager.GetChatRoomsAsync();
            return _mapper.Map<List<ChatRoomDto>>(result);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ChatRoomDto> GetRoom(int id)
        {
            var result = await _chatRoomManager.GetChatRoomByIdAsync(id);
            return _mapper.Map<ChatRoomDto>(result);
        }

        [HttpPost]
        public async Task<ActionResult<ChatRoomDto>> PostRoom(ChatRoomDto room)
        {
            try
            {
                var result = await _chatRoomManager.CreateChatRoomAsync(_mapper.Map<ChatRoom>(room));
                return _mapper.Map<ChatRoomDto>(result);
            }
            catch (CustomException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("linkUser")]
        public async Task<ActionResult<ChatRoomChatUserDto>> LinkUser(ChatRoomChatUserDto room)
        {
            try
            {
                var result = await _chatRoomManager.AddUserToChatRoomAsync(_mapper.Map<ChatRoomChatUser>(room));
                return _mapper.Map<ChatRoomChatUserDto>(result);
            }
            catch (CustomException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("unlinkUser")]
        public async Task<ActionResult> UnlinkUser(ChatRoomChatUserDto room)
        {
            try
            {
                await _chatRoomManager.RemoveUserFromChatRoomAsync(_mapper.Map<ChatRoomChatUser>(room));
                return Ok();
            }
            catch (CustomException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
