using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Contacts.Data.Migrations
{
    public partial class @new : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationUsersContacts",
                table: "ApplicationUsersContacts");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationUsersContacts_ContactId",
                table: "ApplicationUsersContacts");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "WebSite",
                table: "Contacts",
                newName: "Website");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationUsersContacts",
                table: "ApplicationUsersContacts",
                columns: new[] { "ContactId", "ApplicationUserId" });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUsersContacts_ApplicationUserId",
                table: "ApplicationUsersContacts",
                column: "ApplicationUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationUsersContacts",
                table: "ApplicationUsersContacts");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationUsersContacts_ApplicationUserId",
                table: "ApplicationUsersContacts");

            migrationBuilder.RenameColumn(
                name: "Website",
                table: "Contacts",
                newName: "WebSite");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationUsersContacts",
                table: "ApplicationUsersContacts",
                columns: new[] { "ApplicationUserId", "ContactId" });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUsersContacts_ContactId",
                table: "ApplicationUsersContacts",
                column: "ContactId");
        }
    }
}
