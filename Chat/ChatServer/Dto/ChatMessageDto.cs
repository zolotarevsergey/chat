using System.ComponentModel.DataAnnotations;

namespace ChatServer.Dto
{
    public class ChatMessageDto
    {
        public int ChatMessageId { get; set; }

        [Required]
        public int ChatRoomId { get; set; }

        [Required]
        public int ChatUserId { get; set; }

        public string ChatUserName { get; set; }

        [Required]
        public string Message { get; set; }
    }
}
