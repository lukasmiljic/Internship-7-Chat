﻿using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Chat.Data.Entities.Models;
using Chat.Data.Seeds;

namespace Chat.Data.Entities
{
    public class ChatDbContext : DbContext
    {
        public ChatDbContext(DbContextOptions options) : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }
        public DbSet<MessageChannel> MessageChannels => Set<MessageChannel>();
        public DbSet<GroupChannel> GroupChannels => Set<GroupChannel>();
        public DbSet<UserChannel> UserChannels => Set<UserChannel>();
        public DbSet<GroupUser> GroupUsers => Set<GroupUser>();
        public DbSet<Message> Message => Set<Message>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MessageChannel>().UseTptMappingStrategy();

            modelBuilder.Entity<GroupUser>()
                .HasKey(gu => new { gu.UserChannelId, gu.GroupChannelId });

            modelBuilder.Entity<GroupUser>()
            .HasOne(g => g.UserChannel)
            .WithMany(u => u.GroupChannels)
            .HasForeignKey(gu => gu.UserChannelId);

            modelBuilder.Entity<GroupUser>()
                .HasOne(g => g.GroupChannel)
                .WithMany(g => g.Users)
                .HasForeignKey(gu => gu.GroupChannelId);

            modelBuilder.Entity<UserChannel>()
                .Property(uc => uc.IsAdmin)
                .HasDefaultValue(false);

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
            
            DatabaseSeeder.Seed(modelBuilder);
            base.OnModelCreating(modelBuilder);
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
                    .TryGet("connectionStrings:add:ChatDB:connectionString", out var connectionString);

                var options = new DbContextOptionsBuilder<ChatDbContext>()
                    .UseNpgsql(connectionString)
                    .Options;

                return new ChatDbContext(options);
            }
        }
    }
}