using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
	public partial class Mig44 : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AddColumn<string>(
				name: "Image",
				table: "AspNetUsers",
				type: "nvarchar(max)",
				nullable: true);

			migrationBuilder.AddColumn<string>(
				name: "NameSurname",
				table: "AspNetUsers",
				type: "nvarchar(max)",
				nullable: true);
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropColumn(
				name: "Image",
				table: "AspNetUsers");

			migrationBuilder.DropColumn(
				name: "NameSurname",
				table: "AspNetUsers");
		}
	}
}
