using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChatServer.Dto
{
    public class ChatRoomDto
    {
        public int ChatRoomId { get; set; }

        [Required]
        public string Name { get; set; }

        public List<ChatMessageDto> ChatMessages { get; set; }
    }
}
