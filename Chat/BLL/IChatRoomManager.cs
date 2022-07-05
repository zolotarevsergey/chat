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
        Task<ChatRoomChatUser> AddUserToChatRoomAsync(ChatRoomChatUser roomUser);
        Task RemoveUserFromChatRoomAsync(ChatRoomChatUser roomUser);
    }
}
