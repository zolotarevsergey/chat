namespace DAL.DbModels
{
    public class ChatRoomChatUser
    {
        public int ChatRoomId { get; set; }
        public ChatRoom Room { get; set; }
        public int ChatUserId { get; set; }
        public ChatUser User { get; set; }
    }
}
