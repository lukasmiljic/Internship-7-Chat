using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Chat.Data.Migrations
{
    /// <inheritdoc />
    public partial class testtt1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupUser_GroupChannels_GroupChannelId",
                table: "GroupUser");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupUser_UserChannels_UserChannelId",
                table: "GroupUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GroupUser",
                table: "GroupUser");

            migrationBuilder.RenameTable(
                name: "GroupUser",
                newName: "GroupUsers");

            migrationBuilder.RenameIndex(
                name: "IX_GroupUser_GroupChannelId",
                table: "GroupUsers",
                newName: "IX_GroupUsers_GroupChannelId");

            migrationBuilder.AddColumn<string>(
                name: "Body",
                table: "Message",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GroupUsers",
                table: "GroupUsers",
                columns: new[] { "UserChannelId", "GroupChannelId" });

            migrationBuilder.InsertData(
                table: "GroupUsers",
                columns: new[] { "GroupChannelId", "UserChannelId" },
                values: new object[] { 6, 1 });

            migrationBuilder.InsertData(
                table: "Message",
                columns: new[] { "MessageID", "Body", "RecipientFK", "SendTime", "SenderFK" },
                values: new object[,]
                {
                    { 1, "Test message 1", 6, new DateTime(2024, 1, 1, 12, 30, 30, 0, DateTimeKind.Unspecified), 1 },
                    { 2, "Test message 2", 6, new DateTime(2024, 1, 1, 12, 31, 30, 0, DateTimeKind.Unspecified), 1 },
                    { 3, "Test message 3", 6, new DateTime(2024, 1, 1, 12, 32, 30, 0, DateTimeKind.Unspecified), 1 },
                    { 4, "Private channel test message 1", 2, new DateTime(2024, 1, 1, 12, 33, 30, 0, DateTimeKind.Unspecified), 1 },
                    { 5, "Private channel test message 2", 2, new DateTime(2024, 1, 1, 12, 34, 30, 0, DateTimeKind.Unspecified), 1 }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_GroupUsers_GroupChannels_GroupChannelId",
                table: "GroupUsers",
                column: "GroupChannelId",
                principalTable: "GroupChannels",
                principalColumn: "MessageChannelID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupUsers_UserChannels_UserChannelId",
                table: "GroupUsers",
                column: "UserChannelId",
                principalTable: "UserChannels",
                principalColumn: "MessageChannelID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupUsers_GroupChannels_GroupChannelId",
                table: "GroupUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupUsers_UserChannels_UserChannelId",
                table: "GroupUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GroupUsers",
                table: "GroupUsers");

            migrationBuilder.DeleteData(
                table: "GroupUsers",
                keyColumns: new[] { "GroupChannelId", "UserChannelId" },
                keyValues: new object[] { 6, 1 });

            migrationBuilder.DeleteData(
                table: "Message",
                keyColumn: "MessageID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Message",
                keyColumn: "MessageID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Message",
                keyColumn: "MessageID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Message",
                keyColumn: "MessageID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Message",
                keyColumn: "MessageID",
                keyValue: 5);

            migrationBuilder.DropColumn(
                name: "Body",
                table: "Message");

            migrationBuilder.RenameTable(
                name: "GroupUsers",
                newName: "GroupUser");

            migrationBuilder.RenameIndex(
                name: "IX_GroupUsers_GroupChannelId",
                table: "GroupUser",
                newName: "IX_GroupUser_GroupChannelId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GroupUser",
                table: "GroupUser",
                columns: new[] { "UserChannelId", "GroupChannelId" });

            migrationBuilder.AddForeignKey(
                name: "FK_GroupUser_GroupChannels_GroupChannelId",
                table: "GroupUser",
                column: "GroupChannelId",
                principalTable: "GroupChannels",
                principalColumn: "MessageChannelID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupUser_UserChannels_UserChannelId",
                table: "GroupUser",
                column: "UserChannelId",
                principalTable: "UserChannels",
                principalColumn: "MessageChannelID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
