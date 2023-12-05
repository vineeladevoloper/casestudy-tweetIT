using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyTwitterAPI.Migrations
{
    public partial class renameVerifyToAction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Users_VerifiedById",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "VerifiedById",
                table: "Users",
                newName: "ActionById");

            migrationBuilder.RenameIndex(
                name: "IX_Users_VerifiedById",
                table: "Users",
                newName: "IX_Users_ActionById");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Users_ActionById",
                table: "Users",
                column: "ActionById",
                principalTable: "Users",
                principalColumn: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Users_ActionById",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "ActionById",
                table: "Users",
                newName: "VerifiedById");

            migrationBuilder.RenameIndex(
                name: "IX_Users_ActionById",
                table: "Users",
                newName: "IX_Users_VerifiedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Users_VerifiedById",
                table: "Users",
                column: "VerifiedById",
                principalTable: "Users",
                principalColumn: "UserId");
        }
    }
}
