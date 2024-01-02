using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Chat.Data.Migrations
{
    /// <inheritdoc />
    public partial class test7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupUser_GroupChannels_GroupChannelsMessageChannelID",
                table: "GroupUser");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupUser_UserChannels_UsersMessageChannelID",
                table: "GroupUser");

            migrationBuilder.RenameColumn(
                name: "UsersMessageChannelID",
                table: "GroupUser",
                newName: "GroupChannelId");

            migrationBuilder.RenameColumn(
                name: "GroupChannelsMessageChannelID",
                table: "GroupUser",
                newName: "UserChannelId");

            migrationBuilder.RenameIndex(
                name: "IX_GroupUser_UsersMessageChannelID",
                table: "GroupUser",
                newName: "IX_GroupUser_GroupChannelId");

            migrationBuilder.AlterColumn<bool>(
                name: "IsAdmin",
                table: "UserChannels",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.InsertData(
                table: "MessageChannels",
                column: "MessageChannelID",
                values: new object[]
                {
                    1,
                    2,
                    3,
                    4,
                    5,
                    6
                });

            migrationBuilder.InsertData(
                table: "GroupChannels",
                columns: new[] { "MessageChannelID", "Title" },
                values: new object[] { 6, "DevChannel" });

            migrationBuilder.InsertData(
                table: "UserChannels",
                columns: new[] { "MessageChannelID", "Email", "IsAdmin", "Password", "Username" },
                values: new object[] { 1, "admin@mail.com", true, "password", "admin" });

            migrationBuilder.InsertData(
                table: "UserChannels",
                columns: new[] { "MessageChannelID", "Email", "Password", "Username" },
                values: new object[,]
                {
                    { 2, "ante@mail.com", "1234", "ante" },
                    { 3, "bante@mail.com", "1234", "bante" },
                    { 4, "cante@mail.com", "1234", "cante" },
                    { 5, "dante@mail.com", "1234", "dante" }
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupUser_GroupChannels_GroupChannelId",
                table: "GroupUser");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupUser_UserChannels_UserChannelId",
                table: "GroupUser");

            migrationBuilder.DeleteData(
                table: "GroupChannels",
                keyColumn: "MessageChannelID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "UserChannels",
                keyColumn: "MessageChannelID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "UserChannels",
                keyColumn: "MessageChannelID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "UserChannels",
                keyColumn: "MessageChannelID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "UserChannels",
                keyColumn: "MessageChannelID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "UserChannels",
                keyColumn: "MessageChannelID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "MessageChannels",
                keyColumn: "MessageChannelID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "MessageChannels",
                keyColumn: "MessageChannelID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "MessageChannels",
                keyColumn: "MessageChannelID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "MessageChannels",
                keyColumn: "MessageChannelID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "MessageChannels",
                keyColumn: "MessageChannelID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "MessageChannels",
                keyColumn: "MessageChannelID",
                keyValue: 6);

            migrationBuilder.RenameColumn(
                name: "GroupChannelId",
                table: "GroupUser",
                newName: "UsersMessageChannelID");

            migrationBuilder.RenameColumn(
                name: "UserChannelId",
                table: "GroupUser",
                newName: "GroupChannelsMessageChannelID");

            migrationBuilder.RenameIndex(
                name: "IX_GroupUser_GroupChannelId",
                table: "GroupUser",
                newName: "IX_GroupUser_UsersMessageChannelID");

            migrationBuilder.AlterColumn<bool>(
                name: "IsAdmin",
                table: "UserChannels",
                type: "boolean",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupUser_GroupChannels_GroupChannelsMessageChannelID",
                table: "GroupUser",
                column: "GroupChannelsMessageChannelID",
                principalTable: "GroupChannels",
                principalColumn: "MessageChannelID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupUser_UserChannels_UsersMessageChannelID",
                table: "GroupUser",
                column: "UsersMessageChannelID",
                principalTable: "UserChannels",
                principalColumn: "MessageChannelID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
