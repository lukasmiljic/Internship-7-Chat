using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chat.Data.Migrations
{
    /// <inheritdoc />
    public partial class testtesttest33 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Message_MessageChannels_SenderFK",
                table: "Message");

            migrationBuilder.AddForeignKey(
                name: "FK_Message_UserChannels_SenderFK",
                table: "Message",
                column: "SenderFK",
                principalTable: "UserChannels",
                principalColumn: "MessageChannelID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Message_UserChannels_SenderFK",
                table: "Message");

            migrationBuilder.AddForeignKey(
                name: "FK_Message_MessageChannels_SenderFK",
                table: "Message",
                column: "SenderFK",
                principalTable: "MessageChannels",
                principalColumn: "MessageChannelID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
