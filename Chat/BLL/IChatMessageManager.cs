using DAL.DbModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL
{
    public interface IChatMessageManager
    {
        Task<ChatMessage> CreateChatMessageAsync(ChatMessage message);
        Task<List<ChatMessage>> GetChatMessagesAsync();
    }
}
