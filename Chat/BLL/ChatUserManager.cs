using BLL.Exceptions;
using DAL;
using DAL.DbModels;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BLL
{
    public class ChatUserManager : IChatUserManager
    {
        private readonly ChatContext _context;

        public ChatUserManager(ChatContext context)
        {
            _context = context;
        }

        public async Task<ChatUser> CreateChatUserAsync(ChatUser user)
        {
            var userExists = await _context.ChatUsers.AnyAsync(x => x.Name == user.Name);
            if (userExists)
            {
                throw new CustomException("Such user already exists.");
            }

            var result = await _context.ChatUsers.AddAsync(user);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<ChatUser> GetChatUserByNameAsync(string name)
        {
            return await _context.ChatUsers.FirstOrDefaultAsync(x => x.Name == name);
        }

        public async Task<ChatUser> GetChatUserByIdAsync(int id)
        {
            return await _context.ChatUsers.FirstOrDefaultAsync(x => x.ChatUserId == id);
        }
    }
}
