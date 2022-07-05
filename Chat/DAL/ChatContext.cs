using DAL.DbModels;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DAL
{
    public class ChatContext : DbContext
    {
        public DbSet<ChatRoom> ChatRooms { get; set; }
        public DbSet<ChatUser> ChatUsers { get; set; }
        public DbSet<ChatRoomChatUser> ChatRoomChatUsers { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }

        public ChatContext(DbContextOptions<ChatContext> options)
            : base(options)
        {
            if (ChatRoomChatUsers.Count() == 0)
            {
                AddTestData();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChatRoom>()
                .HasIndex(u => u.Name)
                .IsUnique();
            modelBuilder.Entity<ChatUser>()
                .HasIndex(u => u.Name)
                .IsUnique();
            modelBuilder.Entity<ChatMessage>()
                .HasOne(bc => bc.Room)
                .WithMany(b => b.ChatMessages)
                .HasForeignKey(bc => bc.ChatRoomId);
            modelBuilder.Entity<ChatMessage>()
                .HasOne(bc => bc.User)
                .WithMany(c => c.ChatMessages)
                .HasForeignKey(bc => bc.ChatUserId);
            modelBuilder.Entity<ChatRoomChatUser>()
                .HasKey(bc => new { bc.ChatRoomId, bc.ChatUserId });
            modelBuilder.Entity<ChatRoomChatUser>()
                .HasOne(bc => bc.Room)
                .WithMany(b => b.ChatRoomChatUsers)
                .HasForeignKey(bc => bc.ChatRoomId);
            modelBuilder.Entity<ChatRoomChatUser>()
                .HasOne(bc => bc.User)
                .WithMany(c => c.ChatRoomChatUsers)
                .HasForeignKey(bc => bc.ChatUserId);
        }

        private void AddTestData()
        {
            var testUser1 = new ChatUser
            {
                ChatUserId = 1,
                Name = "Luke"
            };

            var testUser2 = new ChatUser
            {
                ChatUserId = 2,
                Name = "Skywalker"
            };

            var testRoom1 = new ChatRoom
            {
                ChatRoomId = 1,
                Name = "Room1"
            };
            var testRoom2 = new ChatRoom
            {
                ChatRoomId = 2,
                Name = "Room2"
            };

            ChatRoomChatUsers.Add(new ChatRoomChatUser { ChatRoomId = 1, ChatUserId = 1, Room = testRoom1, User = testUser1 });
            ChatRoomChatUsers.Add(new ChatRoomChatUser { ChatRoomId = 2, ChatUserId = 2, Room = testRoom2, User = testUser2 });

            ChatMessages.Add(new ChatMessage { ChatMessageId = 1, ChatRoomId = 1, ChatUserId = 1, Room = testRoom1, User = testUser1, Message = "Hello world #1" });
            ChatMessages.Add(new ChatMessage { ChatMessageId = 2, ChatRoomId = 2, ChatUserId = 2, Room = testRoom2, User = testUser2, Message = "Hello world #2" });

            SaveChanges();
        }
    }
}
