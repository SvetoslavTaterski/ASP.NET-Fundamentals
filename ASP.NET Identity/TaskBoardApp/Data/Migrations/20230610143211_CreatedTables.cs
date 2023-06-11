using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskBoardApp.Data.Migrations
{
    public partial class CreatedTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Boards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BoardId = table.Column<int>(type: "int", nullable: false),
                    OwnerId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tasks_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tasks_Boards_BoardId",
                        column: x => x.BoardId,
                        principalTable: "Boards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "7d378153-1532-4cf6-bfb6-ee19a824db0d", 0, "7d1d7aea-0246-4789-8232-57fb4246b783", null, false, false, null, null, "TEST@SOFTUNI.BG", "AQAAAAEAACcQAAAAEKYVBy6BkEAt5O01aavdfdbSOczAjJHHTunbeDlonWKvxiocVF3Lk1LJ5O9LQ14Lsg==", null, false, "3ca06b50-1189-4b04-9284-fb69a8b25015", false, "test@softuni.bg" });

            migrationBuilder.InsertData(
                table: "Boards",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Open" },
                    { 2, "In Progress" },
                    { 3, "Done" }
                });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "BoardId", "CreatedOn", "Description", "OwnerId", "Title" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2022, 11, 22, 17, 32, 11, 668, DateTimeKind.Local).AddTicks(7897), "Implement better styling for all public pages", "7d378153-1532-4cf6-bfb6-ee19a824db0d", "Improve CSS styles" },
                    { 2, 1, new DateTime(2023, 6, 5, 17, 32, 11, 668, DateTimeKind.Local).AddTicks(7929), "Create Android Client App for the TaskBoard RESTful API", "7d378153-1532-4cf6-bfb6-ee19a824db0d", "Android Client App" },
                    { 3, 2, new DateTime(2023, 5, 10, 17, 32, 11, 668, DateTimeKind.Local).AddTicks(7932), "Create Windows Forms desktop app client for the TaskBoard RESTful API", "7d378153-1532-4cf6-bfb6-ee19a824db0d", "Desktop Client App" },
                    { 4, 3, new DateTime(2022, 6, 10, 17, 32, 11, 668, DateTimeKind.Local).AddTicks(7934), "Implement [Create Task] page for adding new tasks", "7d378153-1532-4cf6-bfb6-ee19a824db0d", "Create Tasks" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_BoardId",
                table: "Tasks",
                column: "BoardId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_OwnerId",
                table: "Tasks",
                column: "OwnerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "Boards");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7d378153-1532-4cf6-bfb6-ee19a824db0d");
        }
    }
}
