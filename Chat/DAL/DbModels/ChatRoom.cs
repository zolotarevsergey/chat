using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.DbModels
{
    [Index(nameof(Name), IsUnique = true)]
    public class ChatRoom
    {
        [Key]
        public int ChatRoomId { get; set; }

        public string Name { get; set; }

        public ICollection<ChatUser> ChatUsers { get; set; }

        public ICollection<ChatMessage> ChatMessages { get; set; }
    }
}
