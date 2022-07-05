using BLL.Exceptions;
using DAL;
using DAL.DbModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL
{
    public class ChatMessageManager : IChatMessageManager
    {
        private readonly ChatContext _context;

        public ChatMessageManager(ChatContext context)
        {
            _context = context;
        }

        public async Task<List<ChatMessage>> GetChatMessagesAsync()
        {
            return await _context.ChatMessages.ToListAsync();
        }

        public async Task<ChatMessage> CreateChatMessageAsync(ChatMessage message)
        {
            if (string.IsNullOrEmpty(message.Message))
            {
                throw new CustomException ("Chat message can't be empty!");
            }

            var existingRoom = await _context.ChatRooms.AnyAsync(x => x.ChatRoomId == message.ChatRoomId);
            if (!existingRoom)
            {
                throw new CustomException("Chat room doesn't exist!");
            }

            var existingUser = await _context.ChatUsers.AnyAsync(x => x.ChatUserId == message.ChatUserId);
            if (!existingUser)
            {
                throw new CustomException("Chat user doesn't exist!");
            }

            var existingChatRoomChatUser = await _context.ChatRoomChatUsers.AnyAsync(x => x.ChatUserId == message.ChatUserId
                        && x.ChatRoomId == message.ChatRoomId);
            if (!existingChatRoomChatUser)
            {
                throw new CustomException("There is no such user in this room!");
            }

            var result = await _context.ChatMessages.AddAsync(message);
            await _context.SaveChangesAsync();
            return result.Entity;
        }
    }
}
