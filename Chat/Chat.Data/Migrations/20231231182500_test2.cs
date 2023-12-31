using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chat.Data.Migrations
{
    /// <inheritdoc />
    public partial class test2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SenderMessageChannelID",
                table: "Message",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Message_SenderMessageChannelID",
                table: "Message",
                column: "SenderMessageChannelID");

            migrationBuilder.AddForeignKey(
                name: "FK_Message_MessageChannels_SenderMessageChannelID",
                table: "Message",
                column: "SenderMessageChannelID",
                principalTable: "MessageChannels",
                principalColumn: "MessageChannelID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Message_MessageChannels_SenderMessageChannelID",
                table: "Message");

            migrationBuilder.DropIndex(
                name: "IX_Message_SenderMessageChannelID",
                table: "Message");

            migrationBuilder.DropColumn(
                name: "SenderMessageChannelID",
                table: "Message");
        }
    }
}
