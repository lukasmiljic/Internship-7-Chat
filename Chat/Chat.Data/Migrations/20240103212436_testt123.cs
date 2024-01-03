using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Chat.Data.Migrations
{
    /// <inheritdoc />
    public partial class testt123 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "SendTime",
                table: "Message",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.InsertData(
                table: "Message",
                columns: new[] { "MessageID", "Body", "RecipientFK", "SendTime", "SenderFK" },
                values: new object[,]
                {
                    { 1, "Test message 1", 6, new DateTime(2024, 1, 1, 12, 30, 30, 0, DateTimeKind.Utc), 1 },
                    { 2, "Test message 2", 6, new DateTime(2024, 1, 1, 12, 31, 30, 0, DateTimeKind.Utc), 1 },
                    { 3, "Test message 3", 6, new DateTime(2024, 1, 1, 12, 32, 30, 0, DateTimeKind.Utc), 1 },
                    { 4, "Private channel test message 1", 2, new DateTime(2024, 1, 1, 12, 33, 30, 0, DateTimeKind.Utc), 1 },
                    { 5, "Private channel test message 2", 2, new DateTime(2024, 1, 1, 12, 34, 30, 0, DateTimeKind.Utc), 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AlterColumn<DateTime>(
                name: "SendTime",
                table: "Message",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");
        }
    }
}
