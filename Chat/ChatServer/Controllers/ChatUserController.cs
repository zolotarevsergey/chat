using AutoMapper;
using BLL;
using BLL.Exceptions;
using ChatServer.Dto;
using DAL.DbModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ChatServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChatUserController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IChatUserManager _chatUserManager;

        public ChatUserController(IChatUserManager chatUserManager, IMapper mapper)
        {
            _chatUserManager = chatUserManager;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<ChatUserDto>> PostUser(ChatUserDto user)
        {
            try
            {
                var result = await _chatUserManager.CreateChatUserAsync(_mapper.Map<ChatUser>(user));
                return _mapper.Map<ChatUserDto>(result);
            }
            catch (CustomException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
