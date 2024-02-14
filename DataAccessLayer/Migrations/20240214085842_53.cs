using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class _53 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
            name: "WriterNameSurname",
            table: "Writers");

            migrationBuilder.RenameColumn(
            name: "WriterUserName",
            table: "Writers",
            newName: "WriterNameSurname");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
