using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Chat.Data.Migrations
{
    /// <inheritdoc />
    public partial class test6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MessageChannels",
                columns: table => new
                {
                    MessageChannelID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageChannels", x => x.MessageChannelID);
                });

            migrationBuilder.CreateTable(
                name: "GroupChannels",
                columns: table => new
                {
                    MessageChannelID = table.Column<int>(type: "integer", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupChannels", x => x.MessageChannelID);
                    table.ForeignKey(
                        name: "FK_GroupChannels_MessageChannels_MessageChannelID",
                        column: x => x.MessageChannelID,
                        principalTable: "MessageChannels",
                        principalColumn: "MessageChannelID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Message",
                columns: table => new
                {
                    MessageID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SendTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    SenderFK = table.Column<int>(type: "integer", nullable: false),
                    RecipientFK = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Message", x => x.MessageID);
                    table.ForeignKey(
                        name: "FK_Message_MessageChannels_RecipientFK",
                        column: x => x.RecipientFK,
                        principalTable: "MessageChannels",
                        principalColumn: "MessageChannelID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Message_MessageChannels_SenderFK",
                        column: x => x.SenderFK,
                        principalTable: "MessageChannels",
                        principalColumn: "MessageChannelID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserChannels",
                columns: table => new
                {
                    MessageChannelID = table.Column<int>(type: "integer", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: false),
                    IsAdmin = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserChannels", x => x.MessageChannelID);
                    table.ForeignKey(
                        name: "FK_UserChannels_MessageChannels_MessageChannelID",
                        column: x => x.MessageChannelID,
                        principalTable: "MessageChannels",
                        principalColumn: "MessageChannelID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GroupUser",
                columns: table => new
                {
                    GroupChannelsMessageChannelID = table.Column<int>(type: "integer", nullable: false),
                    UsersMessageChannelID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupUser", x => new { x.GroupChannelsMessageChannelID, x.UsersMessageChannelID });
                    table.ForeignKey(
                        name: "FK_GroupUser_GroupChannels_GroupChannelsMessageChannelID",
                        column: x => x.GroupChannelsMessageChannelID,
                        principalTable: "GroupChannels",
                        principalColumn: "MessageChannelID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupUser_UserChannels_UsersMessageChannelID",
                        column: x => x.UsersMessageChannelID,
                        principalTable: "UserChannels",
                        principalColumn: "MessageChannelID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroupUser_UsersMessageChannelID",
                table: "GroupUser",
                column: "UsersMessageChannelID");

            migrationBuilder.CreateIndex(
                name: "IX_Message_RecipientFK",
                table: "Message",
                column: "RecipientFK");

            migrationBuilder.CreateIndex(
                name: "IX_Message_SenderFK",
                table: "Message",
                column: "SenderFK");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupUser");

            migrationBuilder.DropTable(
                name: "Message");

            migrationBuilder.DropTable(
                name: "GroupChannels");

            migrationBuilder.DropTable(
                name: "UserChannels");

            migrationBuilder.DropTable(
                name: "MessageChannels");
        }
    }
}
