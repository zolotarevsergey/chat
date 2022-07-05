using System.ComponentModel.DataAnnotations;

namespace DAL.DbModels
{
    public class ChatMessage
    {
        [Key]
        public int ChatMessageId { get; set; }

        public int ChatRoomId { get; set; }

        public ChatRoom Room { get; set; }

        public int ChatUserId { get; set; }

        public ChatUser User { get; set; }

        public string Message { get; set; }
    }
}
