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
    [Migration("20240102132757_test6")]
    partial class test6
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Chat.Data.Entities.Models.Message", b =>
                {
                    b.Property<int>("MessageID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("MessageID"));

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

            modelBuilder.Entity("GroupChannelUserChannel", b =>
                {
                    b.Property<int>("GroupChannelsMessageChannelID")
                        .HasColumnType("integer");

                    b.Property<int>("UsersMessageChannelID")
                        .HasColumnType("integer");

                    b.HasKey("GroupChannelsMessageChannelID", "UsersMessageChannelID");

                    b.HasIndex("UsersMessageChannelID");

                    b.ToTable("GroupUser", (string)null);
                });

            modelBuilder.Entity("Chat.Data.Entities.Models.GroupChannel", b =>
                {
                    b.HasBaseType("Chat.Data.Entities.Models.MessageChannel");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.ToTable("GroupChannels");
                });

            modelBuilder.Entity("Chat.Data.Entities.Models.UserChannel", b =>
                {
                    b.HasBaseType("Chat.Data.Entities.Models.MessageChannel");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("boolean");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.ToTable("UserChannels");
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

            modelBuilder.Entity("GroupChannelUserChannel", b =>
                {
                    b.HasOne("Chat.Data.Entities.Models.GroupChannel", null)
                        .WithMany()
                        .HasForeignKey("GroupChannelsMessageChannelID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Chat.Data.Entities.Models.UserChannel", null)
                        .WithMany()
                        .HasForeignKey("UsersMessageChannelID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
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
#pragma warning restore 612, 618
        }
    }
}
