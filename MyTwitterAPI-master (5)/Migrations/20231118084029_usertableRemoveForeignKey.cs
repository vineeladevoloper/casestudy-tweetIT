using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyTwitterAPI.Migrations
{
    public partial class usertableRemoveForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Users_VerifiedById",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_VerifiedById",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserType",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "VerifiedById",
                table: "Users");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserType",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VerifiedById",
                table: "Users",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_VerifiedById",
                table: "Users",
                column: "VerifiedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Users_VerifiedById",
                table: "Users",
                column: "VerifiedById",
                principalTable: "Users",
                principalColumn: "UserId");
        }
    }
}
