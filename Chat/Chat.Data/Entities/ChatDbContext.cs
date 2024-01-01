using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using System.Threading.Channels;
using System.Text.RegularExpressions;
using Chat.Data.Entities.Models;

namespace Chat.Data.Entities
{
    public class ChatDbContext : DbContext
    {
        public ChatDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<MessageChannel> MessageChannels => Set<MessageChannel>();
        public DbSet<GroupChannel> GroupChannels => Set<GroupChannel>();
        public DbSet<UserChannel> UserChannels => Set<UserChannel>();
        public DbSet<Message> Message => Set<Message>();

        //jel mi treba ovo?
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserChannel>()
                .HasMany(x => x.GroupChannels)
                .WithMany(y => y.Users)
                .UsingEntity(j => j.ToTable("GroupUser"));
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Message>()
                .HasOne(x => x.Sender)
                .WithMany(y => y.SentMessages)
                .HasForeignKey(x => x.SenderFK)
                .IsRequired();

            modelBuilder.Entity<Message>()
                .HasOne(x => x.Recipient)
                .WithMany(y => y.RecievedMessages)
                .HasForeignKey(x => x.RecipientFK)
                .IsRequired();
        }

        public class ChatDbContextFactory : IDesignTimeDbContextFactory<ChatDbContext>
        {
            public ChatDbContext CreateDbContext(string[] args)
            {
                var config = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddXmlFile("App.config")
                    .Build();

                config.Providers
                    .First()
                    .TryGet("connectionStrings:add:Chat:connectionString", out var connectionString);

                var options = new DbContextOptionsBuilder<ChatDbContext>()
                    .UseNpgsql(connectionString)
                    .Options;

                return new ChatDbContext(options);
            }
        }
    }
}