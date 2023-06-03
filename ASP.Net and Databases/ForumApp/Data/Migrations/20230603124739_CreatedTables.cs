using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ForumApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreatedTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(1500)", maxLength: 1500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "Content", "Title" },
                values: new object[,]
                {
                    { 1, "Praesent rhoncus luctus ante. Quisque in dolor ut odio efficitur vehicula vel a turpis. Cras ut magna mi. Nam ante.", "My first post" },
                    { 2, "Aliquam erat volutpat. Vestibulum luctus lacus nec diam aliquam egestas. Morbi neque arcu, pharetra eget mi a, lobortis condimentum risus.", "My second post" },
                    { 3, "Nunc sagittis fringilla orci, nec pharetra dolor faucibus a. Nunc vehicula arcu non purus consectetur sagittis. Nunc volutpat tincidunt nisl.", "My third post" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Posts");
        }
    }
}
