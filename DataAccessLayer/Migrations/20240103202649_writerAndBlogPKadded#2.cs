using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
	public partial class writerAndBlogPKadded2 : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey(
				name: "FK_Blogs_Writers_WriterID",
				table: "Blogs");

			migrationBuilder.RenameColumn(
				name: "WriterID",
				table: "Blogs",
				newName: "WriterId");

			migrationBuilder.RenameIndex(
				name: "IX_Blogs_WriterID",
				table: "Blogs",
				newName: "IX_Blogs_WriterId");

			migrationBuilder.AddForeignKey(
				name: "FK_Blogs_Writers_WriterId",
				table: "Blogs",
				column: "WriterId",
				principalTable: "Writers",
				principalColumn: "WriterID",
				onDelete: ReferentialAction.Cascade);
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey(
				name: "FK_Blogs_Writers_WriterId",
				table: "Blogs");

			migrationBuilder.RenameColumn(
				name: "WriterId",
				table: "Blogs",
				newName: "WriterID");

			migrationBuilder.RenameIndex(
				name: "IX_Blogs_WriterId",
				table: "Blogs",
				newName: "IX_Blogs_WriterID");

			migrationBuilder.AddForeignKey(
				name: "FK_Blogs_Writers_WriterID",
				table: "Blogs",
				column: "WriterID",
				principalTable: "Writers",
				principalColumn: "WriterID",
				onDelete: ReferentialAction.Cascade);
		}
	}
}
