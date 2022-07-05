using DAL.DbModels;
using System.Threading.Tasks;

namespace BLL
{
    public interface IChatUserManager
    {
        Task<ChatUser> CreateChatUserAsync(ChatUser user);
        Task<ChatUser> GetChatUserByNameAsync(string name);
        Task<ChatUser> GetChatUserByIdAsync(int id);
    }
}
