using Chat.Data.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                        MessageChannelID = 0,
                        Email = "admin@mail.com",
                        Password = "password",
                        Username = "admin",
                        IsAdmin = true,
                    },
                    new UserChannel()
                    {
                        MessageChannelID = 1,
                        Email = "ante@mail.com",
                        Password = "1234",
                        Username = "ante",
                    },
                    new UserChannel()
                    {
                        MessageChannelID = 2,
                        Email = "bante@mail.com",
                        Password = "1234",
                        Username = "bante",
                    },
                    new UserChannel()
                    {
                        MessageChannelID = 3,
                        Email = "cante@mail.com",
                        Password = "1234",
                        Username = "cante",
                    },
                    new UserChannel()
                    {
                        MessageChannelID = 4,
                        Email = "dante@mail.com",
                        Password = "1234",
                        Username = "dante",
                    }
                });
        }
    }
}
