using BLL.Exceptions;
using DAL;
using DAL.DbModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL
{
    public class ChatRoomManager : IChatRoomManager
    {
        private readonly ChatContext _context;

        public ChatRoomManager(ChatContext context)
        {
            _context = context;
        }

        public async Task<List<ChatRoom>> GetChatRoomsAsync()
        {
            return await _context.ChatRooms.Include(x => x.ChatMessages).ToListAsync();
        }

        public async Task<ChatRoom> GetChatRoomByIdAsync(int id)
        {
            return await _context.ChatRooms
                .Include(x => x.ChatMessages)
                .Include(x => x.ChatUsers)
                .FirstOrDefaultAsync(x => x.ChatRoomId == id);
        }

        public async Task<ChatRoom> GetChatRoomByNameAsync(string name)
        {
            return await _context.ChatRooms.FirstOrDefaultAsync(x => x.Name == name);
        }

        public async Task<ChatRoom> CreateChatRoomAsync(ChatRoom room)
        {
            var roomExists = await _context.ChatRooms.AnyAsync(x => x.Name == room.Name);
            if (!roomExists)
            {
                throw new CustomException("Such room already exists.");
            }

            var result = await _context.ChatRooms.AddAsync(room);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task AddUserToChatRoomAsync(int chatRoomId, int chatUserId)
        {
            var existingRoom = await _context.ChatRooms.Include(x => x.ChatUsers).FirstOrDefaultAsync(x => x.ChatRoomId == chatRoomId);
            if (existingRoom == null)
            {
                throw new CustomException("Such room doesn't exists!");
            }

            var existingUser = await _context.ChatUsers.FirstOrDefaultAsync(x => x.ChatUserId == chatUserId);
            if (existingUser == null)
            {
                throw new CustomException("Such user doesn't exists!");
            }

            var existingLink = existingRoom.ChatUsers.Any(x => x.ChatUserId == chatUserId);
            if (existingLink)
            {
                throw new CustomException("User already exists in this room.");
            }

            existingRoom.ChatUsers.Add(existingUser);
            _context.ChatRooms.Update(existingRoom);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveUserFromChatRoomAsync(int chatRoomId, int chatUserId)
        {
            var existingRoom = await _context.ChatRooms.Include(x => x.ChatUsers).FirstOrDefaultAsync(x => x.ChatRoomId == chatRoomId);
            if (existingRoom == null)
            {
                throw new CustomException("Such room doesn't exists!");
            }

            var existingUser = await _context.ChatUsers.FirstOrDefaultAsync(x => x.ChatUserId == chatUserId);
            if (existingUser == null)
            {
                throw new CustomException("Such user doesn't exists!");
            }

            var existingLink = existingRoom.ChatUsers.Any(x => x.ChatUserId == chatUserId);
            if (!existingLink)
            {
                throw new CustomException("User doesn't linked in this room.");
            }

            existingRoom.ChatUsers.Remove(existingUser);
            _context.ChatRooms.Update(existingRoom);
            await _context.SaveChangesAsync();
        }
    }
}
