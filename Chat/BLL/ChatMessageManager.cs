using BLL.Exceptions;
using DAL;
using DAL.DbModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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

            var existingRoom = await _context.ChatRooms.Include(x => x.ChatUsers).FirstOrDefaultAsync(x => x.ChatRoomId == message.ChatRoomId);
            if (existingRoom == null)
            {
                throw new CustomException("Chat room doesn't exist!");
            }

            var existingUser = await _context.ChatUsers.FirstOrDefaultAsync(x => x.ChatUserId == message.ChatUserId);
            if (existingUser == null)
            {
                throw new CustomException("Chat user doesn't exist!");
            }

            var existingLink = existingRoom.ChatUsers.Any(x => x.ChatUserId == message.ChatUserId);
            if (!existingLink)
            {
                throw new CustomException("There is no such user in this room!");
            }

            message.Room = existingRoom;
            message.User = existingUser;
            var result = await _context.ChatMessages.AddAsync(message);
            await _context.SaveChangesAsync();
            return result.Entity;
        }
    }
}
