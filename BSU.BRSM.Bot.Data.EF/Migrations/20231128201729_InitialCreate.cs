using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BSU.BRSM.Bot.Data.EF.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ChatId = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", nullable: true),
                    UserName = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ChatId);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IsEnded = table.Column<bool>(type: "INTEGER", nullable: false),
                    Body = table.Column<string>(type: "TEXT", nullable: true),
                    Answer = table.Column<string>(type: "TEXT", nullable: true),
                    IsPosted = table.Column<bool>(type: "INTEGER", nullable: false),
                    MessageId = table.Column<int>(type: "INTEGER", nullable: false),
                    DateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UserChatId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Questions_Users_UserChatId",
                        column: x => x.UserChatId,
                        principalTable: "Users",
                        principalColumn: "ChatId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "ChatId", "FirstName", "UserName" },
                values: new object[,]
                {
                    { 87232694547L, "Гринь", "Макс" },
                    { 87235656834L, "Green", "MaX" },
                    { 87235698347L, "Гринь", "Максим" }
                });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "Answer", "Body", "DateTime", "IsEnded", "IsPosted", "MessageId", "UserChatId" },
                values: new object[,]
                {
                    { 1, null, "Как дела?", new DateTime(2023, 11, 28, 23, 17, 29, 510, DateTimeKind.Local).AddTicks(809), true, false, 1, 87235698347L },
                    { 2, null, "Как твои", new DateTime(2023, 11, 28, 23, 17, 29, 510, DateTimeKind.Local).AddTicks(824), false, false, 2, 87235698347L },
                    { 3, null, "Что делаешь?", new DateTime(2023, 11, 28, 23, 17, 29, 510, DateTimeKind.Local).AddTicks(826), true, false, 1, 87232694547L },
                    { 4, null, "Какая сегодня", new DateTime(2023, 11, 28, 23, 17, 29, 510, DateTimeKind.Local).AddTicks(827), false, false, 2, 87232694547L },
                    { 5, null, "Ты покушал?", new DateTime(2023, 11, 28, 23, 17, 29, 510, DateTimeKind.Local).AddTicks(829), true, false, 1, 87235656834L },
                    { 6, null, "Ты пок", new DateTime(2023, 11, 28, 23, 17, 29, 510, DateTimeKind.Local).AddTicks(830), false, false, 2, 87235656834L }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Questions_UserChatId",
                table: "Questions",
                column: "UserChatId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
