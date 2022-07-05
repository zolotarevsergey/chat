using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.DbModels
{
    [Index(nameof(Name), IsUnique = true)]
    public class ChatUser
    {
        [Key]
        public int ChatUserId { get; set; }

        public string Name { get; set; }

        public ICollection<ChatRoomChatUser> ChatRoomChatUsers { get; set; }

        public ICollection<ChatMessage> ChatMessages { get; set; }
    }
}
