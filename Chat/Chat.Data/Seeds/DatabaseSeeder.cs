using Chat.Data.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Chat.Data.Seeds
{
    public static class DatabaseSeeder
    {
        public static void Seed(ModelBuilder builder) 
        {
            builder.Entity<UserChannel>()
                .HasData(new List<UserChannel>()
                {
                    new UserChannel()
                    {
                        MessageChannelID = 1,
                        Email = "admin@mail.com",
                        Password = "password",
                        Username = "admin",
                        IsAdmin = true,
                    },
                    new UserChannel()
                    {
                        MessageChannelID = 2,
                        Email = "ante@mail.com",
                        Password = "1234",
                        Username = "ante",
                    },
                    new UserChannel()
                    {
                        MessageChannelID = 3,
                        Email = "bante@mail.com",
                        Password = "1234",
                        Username = "bante",
                    },
                    new UserChannel()
                    {
                        MessageChannelID = 4,
                        Email = "cante@mail.com",
                        Password = "1234",
                        Username = "cante",
                    },
                    new UserChannel()
                    {
                        MessageChannelID = 5,
                        Email = "dante@mail.com",
                        Password = "1234",
                        Username = "dante",
                    }
                });

            builder.Entity<GroupChannel>()
                .HasData(new List<GroupChannel>()
                {
                    new GroupChannel()
                    {
                        MessageChannelID = 6,
                        Title = "DevChannel",
                    }
                });

            builder.Entity<GroupUser>()
                .HasData(new List<GroupUser>()
                {
                    new GroupUser()
                    {
                        GroupChannelId = 6,
                        UserChannelId = 1,
                    }
                });

            builder.Entity<Message>()
                .HasData(new List<Message>()
                {
                    new Message()
                    {
                        MessageID = 1,
                        SendTime = new DateTime(2024,1,1,12,30,30),
                        SenderFK = 1,
                        RecipientFK = 6,
                        Body = "Test message 1"
                    },
                    new Message()
                    {
                        MessageID = 2,
                        SendTime = new DateTime(2024,1,1,12,31,30),
                        SenderFK = 1,
                        RecipientFK = 6,
                        Body = "Test message 2"
                    },
                    new Message()
                    {
                        MessageID = 3,
                        SendTime = new DateTime(2024,1,1,12,32,30),
                        SenderFK = 1,
                        RecipientFK = 6,
                        Body = "Test message 3"
                    },
                    new Message()
                    {
                        MessageID = 4,
                        SendTime = new DateTime(2024,1,1,12,33,30),
                        SenderFK = 1,
                        RecipientFK = 2,
                        Body = "Private channel test message 1"
                    },
                    new Message()
                    {
                        MessageID = 5,
                        SendTime = new DateTime(2024,1,1,12,34,30),
                        SenderFK = 1,
                        RecipientFK = 2,
                        Body = "Private channel test message 2"
                    }
                }) ;
        }
    }
}