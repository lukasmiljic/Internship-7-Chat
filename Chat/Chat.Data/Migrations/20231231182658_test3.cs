using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chat.Data.Migrations
{
    /// <inheritdoc />
    public partial class test3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "GroupChannelUserChannel",
                columns: table => new
                {
                    GroupChannelsMessageChannelID = table.Column<int>(type: "integer", nullable: false),
                    UsersMessageChannelID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupChannelUserChannel", x => new { x.GroupChannelsMessageChannelID, x.UsersMessageChannelID });
                    table.ForeignKey(
                        name: "FK_GroupChannelUserChannel_MessageChannels_GroupChannelsMessag~",
                        column: x => x.GroupChannelsMessageChannelID,
                        principalTable: "MessageChannels",
                        principalColumn: "MessageChannelID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupChannelUserChannel_MessageChannels_UsersMessageChannel~",
                        column: x => x.UsersMessageChannelID,
                        principalTable: "MessageChannels",
                        principalColumn: "MessageChannelID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroupChannelUserChannel_UsersMessageChannelID",
                table: "GroupChannelUserChannel",
                column: "UsersMessageChannelID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupChannelUserChannel");

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
    }
}
