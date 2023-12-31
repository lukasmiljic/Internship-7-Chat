using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chat.Data.Migrations
{
    /// <inheritdoc />
    public partial class test4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupChannelUserChannel_MessageChannels_GroupChannelsMessag~",
                table: "GroupChannelUserChannel");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupChannelUserChannel_MessageChannels_UsersMessageChannel~",
                table: "GroupChannelUserChannel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GroupChannelUserChannel",
                table: "GroupChannelUserChannel");

            migrationBuilder.RenameTable(
                name: "GroupChannelUserChannel",
                newName: "GroupUser");

            migrationBuilder.RenameIndex(
                name: "IX_GroupChannelUserChannel_UsersMessageChannelID",
                table: "GroupUser",
                newName: "IX_GroupUser_UsersMessageChannelID");

            migrationBuilder.AddColumn<int>(
                name: "RecipientFK",
                table: "Message",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SenderFK",
                table: "Message",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_GroupUser",
                table: "GroupUser",
                columns: new[] { "GroupChannelsMessageChannelID", "UsersMessageChannelID" });

            migrationBuilder.CreateIndex(
                name: "IX_Message_RecipientFK",
                table: "Message",
                column: "RecipientFK");

            migrationBuilder.CreateIndex(
                name: "IX_Message_SenderFK",
                table: "Message",
                column: "SenderFK");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupUser_MessageChannels_GroupChannelsMessageChannelID",
                table: "GroupUser",
                column: "GroupChannelsMessageChannelID",
                principalTable: "MessageChannels",
                principalColumn: "MessageChannelID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupUser_MessageChannels_UsersMessageChannelID",
                table: "GroupUser",
                column: "UsersMessageChannelID",
                principalTable: "MessageChannels",
                principalColumn: "MessageChannelID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Message_MessageChannels_RecipientFK",
                table: "Message",
                column: "RecipientFK",
                principalTable: "MessageChannels",
                principalColumn: "MessageChannelID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Message_MessageChannels_SenderFK",
                table: "Message",
                column: "SenderFK",
                principalTable: "MessageChannels",
                principalColumn: "MessageChannelID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupUser_MessageChannels_GroupChannelsMessageChannelID",
                table: "GroupUser");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupUser_MessageChannels_UsersMessageChannelID",
                table: "GroupUser");

            migrationBuilder.DropForeignKey(
                name: "FK_Message_MessageChannels_RecipientFK",
                table: "Message");

            migrationBuilder.DropForeignKey(
                name: "FK_Message_MessageChannels_SenderFK",
                table: "Message");

            migrationBuilder.DropIndex(
                name: "IX_Message_RecipientFK",
                table: "Message");

            migrationBuilder.DropIndex(
                name: "IX_Message_SenderFK",
                table: "Message");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GroupUser",
                table: "GroupUser");

            migrationBuilder.DropColumn(
                name: "RecipientFK",
                table: "Message");

            migrationBuilder.DropColumn(
                name: "SenderFK",
                table: "Message");

            migrationBuilder.RenameTable(
                name: "GroupUser",
                newName: "GroupChannelUserChannel");

            migrationBuilder.RenameIndex(
                name: "IX_GroupUser_UsersMessageChannelID",
                table: "GroupChannelUserChannel",
                newName: "IX_GroupChannelUserChannel_UsersMessageChannelID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GroupChannelUserChannel",
                table: "GroupChannelUserChannel",
                columns: new[] { "GroupChannelsMessageChannelID", "UsersMessageChannelID" });

            migrationBuilder.AddForeignKey(
                name: "FK_GroupChannelUserChannel_MessageChannels_GroupChannelsMessag~",
                table: "GroupChannelUserChannel",
                column: "GroupChannelsMessageChannelID",
                principalTable: "MessageChannels",
                principalColumn: "MessageChannelID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupChannelUserChannel_MessageChannels_UsersMessageChannel~",
                table: "GroupChannelUserChannel",
                column: "UsersMessageChannelID",
                principalTable: "MessageChannels",
                principalColumn: "MessageChannelID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
