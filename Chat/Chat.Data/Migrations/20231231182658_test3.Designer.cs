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
    [Migration("20231231182658_test3")]
    partial class test3
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

                    b.Property<DateTime>("SendTime")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("MessageID");

                    b.ToTable("Message");
                });

            modelBuilder.Entity("Chat.Data.Entities.Models.MessageChannel", b =>
                {
                    b.Property<int>("MessageChannelID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("MessageChannelID"));

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(21)
                        .HasColumnType("character varying(21)");

                    b.HasKey("MessageChannelID");

                    b.ToTable("MessageChannels");

                    b.HasDiscriminator<string>("Discriminator").HasValue("MessageChannel");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("GroupChannelUserChannel", b =>
                {
                    b.Property<int>("GroupChannelsMessageChannelID")
                        .HasColumnType("integer");

                    b.Property<int>("UsersMessageChannelID")
                        .HasColumnType("integer");

                    b.HasKey("GroupChannelsMessageChannelID", "UsersMessageChannelID");

                    b.HasIndex("UsersMessageChannelID");

                    b.ToTable("GroupChannelUserChannel");
                });

            modelBuilder.Entity("Chat.Data.Entities.Models.GroupChannel", b =>
                {
                    b.HasBaseType("Chat.Data.Entities.Models.MessageChannel");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasDiscriminator().HasValue("GroupChannel");
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

                    b.HasDiscriminator().HasValue("UserChannel");
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
#pragma warning restore 612, 618
        }
    }
}