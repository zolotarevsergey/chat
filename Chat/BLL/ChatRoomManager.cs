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
                .Include(x => x.ChatRoomChatUsers)
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

        public async Task<ChatRoomChatUser> AddUserToChatRoomAsync(ChatRoomChatUser roomUser)
        {
            var roomExists = await _context.ChatRooms.AnyAsync(x => x.ChatRoomId == roomUser.ChatRoomId);
            if (!roomExists)
            {
                throw new CustomException("Such room doesn't exists!");
            }

            var userExists = await _context.ChatUsers.AnyAsync(x => x.ChatUserId == roomUser.ChatUserId);
            if (!userExists)
            {
                throw new CustomException("Such user doesn't exists!");
            }

            var roomUserExists = await _context.ChatRoomChatUsers.AnyAsync(x => x.ChatUserId == roomUser.ChatUserId &&
                        x.ChatRoomId == roomUser.ChatRoomId);
            if (roomUserExists)
            {
                throw new CustomException("User already exists in this room.");
            }

            var result = await _context.ChatRoomChatUsers.AddAsync(roomUser);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task RemoveUserFromChatRoomAsync(ChatRoomChatUser roomUser)
        {
            var roomExists = await _context.ChatRooms.AnyAsync(x => x.ChatRoomId == roomUser.ChatRoomId);
            if (!roomExists)
            {
                throw new CustomException("Such room doesn't exists!");
            }

            var userExists = await _context.ChatUsers.AnyAsync(x => x.ChatUserId == roomUser.ChatUserId);
            if (!userExists)
            {
                throw new CustomException("Such user doesn't exists!");
            }

            var existingUser = await _context.ChatRoomChatUsers.FirstOrDefaultAsync(x => x.ChatUserId == roomUser.ChatUserId &&
                        x.ChatRoomId == roomUser.ChatRoomId);
            if (existingUser == null)
            {
                throw new CustomException("User doesn't exist in this room!");
            }

            var result = _context.ChatRoomChatUsers.Remove(existingUser);
            await _context.SaveChangesAsync();
        }
    }
}
