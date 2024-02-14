using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class mig52 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WriterName",
                table: "Writers",
                newName: "WriterUserName");

            migrationBuilder.AddColumn<int>(
                name: "UserID",
                table: "Writers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WriterNameSurname",
                table: "Writers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Writers_AspNetUsers_UserID",
                table: "Writers",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Writers_AspNetUsers_UserID",
                table: "Writers");

            migrationBuilder.DropIndex(
                name: "IX_Writers_UserID",
                table: "Writers");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Writers");

            migrationBuilder.DropColumn(
                name: "WriterNameSurname",
                table: "Writers");

            migrationBuilder.RenameColumn(
                name: "WriterUserName",
                table: "Writers",
                newName: "WriterName");
        }
    }
}
