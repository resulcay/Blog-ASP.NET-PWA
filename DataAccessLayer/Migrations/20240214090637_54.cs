using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class _54 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
            name: "WriterUserName",
            table: "Writers",
            nullable: false,
            defaultValue: "default");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
