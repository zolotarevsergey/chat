using DAL.DbModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL
{
    public interface IChatRoomManager
    {
        Task<List<ChatRoom>> GetChatRoomsAsync();
        Task<ChatRoom> GetChatRoomByIdAsync(int id);
        Task<ChatRoom> CreateChatRoomAsync(ChatRoom room);
        Task<ChatRoom> GetChatRoomByNameAsync(string name);
        Task AddUserToChatRoomAsync(int chatRoomId, int chatUserId);
        Task RemoveUserFromChatRoomAsync(int chatRoomId, int chatUserId);
    }
}
