﻿// <auto-generated />
using System;
using Chat.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Chat.Data.Migrations
{
    [DbContext(typeof(ChatDbContext))]
    [Migration("20240103191154_testtt1")]
    partial class testtt1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Chat.Data.Entities.Models.GroupUser", b =>
                {
                    b.Property<int>("UserChannelId")
                        .HasColumnType("integer");

                    b.Property<int>("GroupChannelId")
                        .HasColumnType("integer");

                    b.HasKey("UserChannelId", "GroupChannelId");

                    b.HasIndex("GroupChannelId");

                    b.ToTable("GroupUsers");

                    b.HasData(
                        new
                        {
                            UserChannelId = 1,
                            GroupChannelId = 6
                        });
                });

            modelBuilder.Entity("Chat.Data.Entities.Models.Message", b =>
                {
                    b.Property<int>("MessageID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("MessageID"));

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("RecipientFK")
                        .HasColumnType("integer");

                    b.Property<DateTime>("SendTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("SenderFK")
                        .HasColumnType("integer");

                    b.HasKey("MessageID");

                    b.HasIndex("RecipientFK");

                    b.HasIndex("SenderFK");

                    b.ToTable("Message");

                    b.HasData(
                        new
                        {
                            MessageID = 1,
                            Body = "Test message 1",
                            RecipientFK = 6,
                            SendTime = new DateTime(2024, 1, 1, 12, 30, 30, 0, DateTimeKind.Unspecified),
                            SenderFK = 1
                        },
                        new
                        {
                            MessageID = 2,
                            Body = "Test message 2",
                            RecipientFK = 6,
                            SendTime = new DateTime(2024, 1, 1, 12, 31, 30, 0, DateTimeKind.Unspecified),
                            SenderFK = 1
                        },
                        new
                        {
                            MessageID = 3,
                            Body = "Test message 3",
                            RecipientFK = 6,
                            SendTime = new DateTime(2024, 1, 1, 12, 32, 30, 0, DateTimeKind.Unspecified),
                            SenderFK = 1
                        },
                        new
                        {
                            MessageID = 4,
                            Body = "Private channel test message 1",
                            RecipientFK = 2,
                            SendTime = new DateTime(2024, 1, 1, 12, 33, 30, 0, DateTimeKind.Unspecified),
                            SenderFK = 1
                        },
                        new
                        {
                            MessageID = 5,
                            Body = "Private channel test message 2",
                            RecipientFK = 2,
                            SendTime = new DateTime(2024, 1, 1, 12, 34, 30, 0, DateTimeKind.Unspecified),
                            SenderFK = 1
                        });
                });

            modelBuilder.Entity("Chat.Data.Entities.Models.MessageChannel", b =>
                {
                    b.Property<int>("MessageChannelID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("MessageChannelID"));

                    b.HasKey("MessageChannelID");

                    b.ToTable("MessageChannels");

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("Chat.Data.Entities.Models.GroupChannel", b =>
                {
                    b.HasBaseType("Chat.Data.Entities.Models.MessageChannel");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.ToTable("GroupChannels");

                    b.HasData(
                        new
                        {
                            MessageChannelID = 6,
                            Title = "DevChannel"
                        });
                });

            modelBuilder.Entity("Chat.Data.Entities.Models.UserChannel", b =>
                {
                    b.HasBaseType("Chat.Data.Entities.Models.MessageChannel");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsAdmin")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.ToTable("UserChannels");

                    b.HasData(
                        new
                        {
                            MessageChannelID = 1,
                            Email = "admin@mail.com",
                            IsAdmin = true,
                            Password = "password",
                            Username = "admin"
                        },
                        new
                        {
                            MessageChannelID = 2,
                            Email = "ante@mail.com",
                            IsAdmin = false,
                            Password = "1234",
                            Username = "ante"
                        },
                        new
                        {
                            MessageChannelID = 3,
                            Email = "bante@mail.com",
                            IsAdmin = false,
                            Password = "1234",
                            Username = "bante"
                        },
                        new
                        {
                            MessageChannelID = 4,
                            Email = "cante@mail.com",
                            IsAdmin = false,
                            Password = "1234",
                            Username = "cante"
                        },
                        new
                        {
                            MessageChannelID = 5,
                            Email = "dante@mail.com",
                            IsAdmin = false,
                            Password = "1234",
                            Username = "dante"
                        });
                });

            modelBuilder.Entity("Chat.Data.Entities.Models.GroupUser", b =>
                {
                    b.HasOne("Chat.Data.Entities.Models.GroupChannel", "GroupChannel")
                        .WithMany("Users")
                        .HasForeignKey("GroupChannelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Chat.Data.Entities.Models.UserChannel", "UserChannel")
                        .WithMany("GroupChannels")
                        .HasForeignKey("UserChannelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GroupChannel");

                    b.Navigation("UserChannel");
                });

            modelBuilder.Entity("Chat.Data.Entities.Models.Message", b =>
                {
                    b.HasOne("Chat.Data.Entities.Models.MessageChannel", "Recipient")
                        .WithMany("RecievedMessages")
                        .HasForeignKey("RecipientFK")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Chat.Data.Entities.Models.MessageChannel", "Sender")
                        .WithMany("SentMessages")
                        .HasForeignKey("SenderFK")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Recipient");

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("Chat.Data.Entities.Models.GroupChannel", b =>
                {
                    b.HasOne("Chat.Data.Entities.Models.MessageChannel", null)
                        .WithOne()
                        .HasForeignKey("Chat.Data.Entities.Models.GroupChannel", "MessageChannelID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Chat.Data.Entities.Models.UserChannel", b =>
                {
                    b.HasOne("Chat.Data.Entities.Models.MessageChannel", null)
                        .WithOne()
                        .HasForeignKey("Chat.Data.Entities.Models.UserChannel", "MessageChannelID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Chat.Data.Entities.Models.MessageChannel", b =>
                {
                    b.Navigation("RecievedMessages");

                    b.Navigation("SentMessages");
                });

            modelBuilder.Entity("Chat.Data.Entities.Models.GroupChannel", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("Chat.Data.Entities.Models.UserChannel", b =>
                {
                    b.Navigation("GroupChannels");
                });
#pragma warning restore 612, 618
        }
    }
}
