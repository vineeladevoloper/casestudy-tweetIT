using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyTwitterAPI.Migrations
{
    public partial class renameUserTyprtoType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserType",
                table: "Users",
                newName: "Type");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Users",
                newName: "UserType");
        }
    }
}
