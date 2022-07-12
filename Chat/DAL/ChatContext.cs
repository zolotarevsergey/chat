using DAL.DbModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DAL
{
    public class ChatContext : DbContext
    {
        public DbSet<ChatRoom> ChatRooms { get; set; }
        public DbSet<ChatUser> ChatUsers { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }

        public ChatContext(DbContextOptions<ChatContext> options)
            : base(options)
        {
            if (ChatRooms.Count() == 0)
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
        }

        private void AddTestData()
        {
            var testUser1 = new ChatUser
            {
                Name = "Luke"
            };

            var testUser2 = new ChatUser
            {
                Name = "Skywalker"
            };

            var testRoom1 = new ChatRoom
            {
                Name = "Room1",
                ChatUsers = new List<ChatUser> { testUser1 }
            };
            var testRoom2 = new ChatRoom
            {
                Name = "Room2",
                ChatUsers = new List<ChatUser> { testUser2 }
            };

            AddRange(testRoom1, testRoom2,
                     new ChatMessage { Room = testRoom1, User = testUser1, Message = "Hello world #1" },
                     new ChatMessage { Room = testRoom2, User = testUser2, Message = "Hello world #2" });

            SaveChanges();
        }
    }
}
